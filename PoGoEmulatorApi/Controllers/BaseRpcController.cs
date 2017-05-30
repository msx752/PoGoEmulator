using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Google.Protobuf;
using Google.Protobuf.Collections;
using log4net;
using Microsoft.EntityFrameworkCore;
using PoGoEmulatorApi.Database;
using PoGoEmulatorApi.Database.Tables;
using PoGoEmulatorApi.Enums;
using PoGoEmulatorApi.Models;
using PoGoEmulatorApi.Responses;
using PoGoEmulatorApi.Responses.Packets;
using POGOProtos.Enums;
using POGOProtos.Inventory;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using POGOProtos.Settings;
using ITraceWriter = System.Web.Http.Tracing.ITraceWriter;

namespace PoGoEmulatorApi.Controllers
{
    public class BaseRpcController : ApiController
    {
        public PoGoDbContext Database { get; set; }
        public ILog Log { get; set; } = null;
        public HttpContext RpcContext { get { return HttpContext.Current; } }
        public HttpResponse Response { get { return RpcContext.Response; } }
        public new HttpRequest Request { get { return RpcContext.Request; } }
        protected Exception CurrentException { get; set; }
        private RequestEnvelope _requestProto;

        public HttpRequestMessage HttpRequestMessage { get { return RpcContext.Items["MS_HttpRequestMessage"] as HttpRequestMessage; } }

        public BaseRpcController(PoGoDbContext db)
        {
            this.Database = db;
        }

        public RequestEnvelope ProtoRequest
        {
            get
            {
                LoadProtoContent();
                return _requestProto;
            }
            set { _requestProto = value; }
        }

        public ResponseEnvelope ProtoResponse { get; set; } = new ResponseEnvelope();
        public RpcRequestType RpcType { get; set; } = RpcRequestType.None;

        public virtual HttpResponseMessage Rpc()
        {
            return this.AnswerToUser(HttpStatusCode.OK);
        }

        protected void LoadProtoContent()
        {
            if (_requestProto != null) return;
            if (RpcType == RpcRequestType.None) return;

            _requestProto = new RequestEnvelope();
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);

            Byte[] buf = new byte[req.Length];
            req.Read(buf, 0, buf.Length);
            Google.Protobuf.CodedInputStream cis = new CodedInputStream(buf);
            _requestProto.MergeFrom(cis);
        }

        protected HttpResponseMessage AnswerToUser(HttpStatusCode code)
        {
            HttpResponseMessage res = new HttpResponseMessage(code);
            res.Headers.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
            res.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            res.Headers.TryAddWithoutValidation("PoGoEmulator", ".NET 4.6.2 MVC API");
            res.Headers.TryAddWithoutValidation("Date", $"{string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT");
            if (code == HttpStatusCode.OK)
            {
                res.Content = new ByteArrayContent(ProtoResponse.ToByteArray());
                Database.SaveChanges();
                Log.Dbg($"succesfully responding");
            }
            else
            {
                Log.Dbg($"unsuccesfully responding error: {CurrentException.Message}");
                res.Content = new StringContent(CurrentException.Message);
            }
            return res;
        }

        public HttpResponseMessage EnvelopResponse(RepeatedField<ByteString> returns = null)
        {
            if (returns != null)
            {
                this.Log.Dbg($"ReturnsCount:{returns.Count}");
                this.ProtoResponse.Returns.AddRange(returns);
            }
            else
            {
                this.Log.Dbg($"ReturnsCount:null");
            }

            if (this.ProtoRequest.AuthTicket != null)
            {
                this.Log.Dbg($"brcontroller.ProtoRequest.AuthTicket: {this.ProtoRequest.AuthTicket}");
                this.ProtoResponse.AuthTicket = new AuthTicket() { };
            }

            this.Log.Dbg($"brcontroller.ProtoResponse.Unknown6.ResponseType: ADDED 1");
            this.ProtoResponse.Unknown6.Add(new Unknown6Response()
            {
                ResponseType = 6,
                Unknown2 = new Unknown6Response.Types.Unknown2()
                {
                    Unknown1 = 1
                }
            });
            this.ProtoResponse.StatusCode = 1;
            return this.Rpc();
        }

        public RepeatedField<ByteString> ProcessRequests()
        {
            RepeatedField<ByteString> Body = new RepeatedField<ByteString>();

            this.Log.Dbg($"brcontroller.ProtoRequest.Requests.Count: {this.ProtoRequest.Requests.Count}");
            foreach (var req in this.ProtoRequest.Requests)
            {
                Body.Add(this.ProcessResponse(req));
            }
            return Body;
        }

        public ByteString ProcessResponse(Request req)
        {
            try
            {
                var type = req.RequestType;
                CodedInputStream codedStream = new CodedInputStream(req.RequestMessage.ToByteArray());
                var strType = $"POGOProtos.Networking.Requests.Messages.{type}Message";
                object msg = Activator.CreateInstance(FindTypeOfObject(strType));
                MethodInfo methodMergeFrom = msg?.GetType()
                    .GetMethods()
                    .ToList()
                    .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");

                methodMergeFrom.Invoke(msg, new object[] { codedStream });

                this.Log.Dbg($"TypeOfRequestMessage: {strType}");
                switch (type)
                {
                    //player
                    case RequestType.SetAvatar:
                    case RequestType.GetPlayer:
                    case RequestType.GetInventory:
                    case RequestType.ReleasePokemon:
                    case RequestType.UpgradePokemon:
                    case RequestType.GetAssetDigest:
                    case RequestType.NicknamePokemon:
                    case RequestType.ClaimCodename:
                    case RequestType.GetHatchedEggs:
                    case RequestType.LevelUpRewards:
                    case RequestType.GetPlayerProfile:
                    case RequestType.CheckAwardedBadges:
                    case RequestType.SetFavoritePokemon:
                    case RequestType.RecycleInventoryItem:
                        return this.GetPacket(type, msg);

                    //global
                    case RequestType.Encounter:
                    case RequestType.FortSearch:
                    case RequestType.FortDetails:
                    case RequestType.CatchPokemon:
                    case RequestType.GetMapObjects:
                    case RequestType.CheckChallenge:
                    case RequestType.GetDownloadUrls:
                    case RequestType.DownloadSettings:
                    case RequestType.DownloadRemoteConfigVersion:
                    case RequestType.DownloadItemTemplates:
                    case RequestType.MarkTutorialComplete:
                        return this.GetGlobalPacket(type, msg);

                    default:
                        throw new Exception($"unknown request Type:{type}");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Type FindTypeOfObject(string qualifiedTypeName)
        {
            var t = Type.GetType(qualifiedTypeName);
            if (t != null)
                return t;
            else
            {
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = asm.GetType(qualifiedTypeName);
                    if (t != null)
                        return t;
                }
                return null;
            }
        }

        public ByteString GetGlobalPacket(RequestType typ, object msg)
        {
            switch (typ)
            {
                //case RequestType.Encounter:
                //    var pl = (EncounterMessage)msg;
                //    EncounterResponse en = new EncounterResponse();
                //    return en.ToByteString();

                //case RequestType.CatchPokemon:
                //    CatchPokemonResponse cp = new CatchPokemonResponse();
                //    return cp.ToByteString();

                //case RequestType.FortSearch:
                //    FortSearchResponse fs = new FortSearchResponse();
                //    return fs.ToByteString();

                //case RequestType.FortDetails:
                //    FortDetailsResponse fd = new FortDetailsResponse();
                //    return fd.ToByteString();

                //case RequestType.GetMapObjects:
                //    GetMapObjectsResponse gmo = new GetMapObjectsResponse();
                //    return gmo.ToByteString();

                case RequestType.CheckChallenge:
                    var c = new CheckChallengeResponse()
                    {
                        ChallengeUrl = " "
                    };

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(CheckChallengeResponse)}");
                    return c.ToByteString();

                //case RequestType.GetDownloadUrls:
                //    GetDownloadUrlsResponse gdu = new GetDownloadUrlsResponse();
                //    return gdu.ToByteString();

                case RequestType.DownloadSettings:
                    DownloadSettingsResponse ds = new DownloadSettingsResponse();
                    ds.Hash = "2788184af4004004d6ab0740f7632983332106f6";
                    ds.Settings = new POGOProtos.Settings.GlobalSettings()
                    {
                        FortSettings = new FortSettings()
                        {
                            InteractionRangeMeters = 40.25098039215686,
                            MaxTotalDeployedPokemon = 10,
                            MaxPlayerDeployedPokemon = 1,
                            DeployStaminaMultiplier = 8.062745098039215,
                            DeployAttackMultiplier = 0,
                            FarInteractionRangeMeters = 1000.0156862745098
                        },
                        MapSettings = new MapSettings()
                        {
                            PokemonVisibleRange = 999.00196078431372,
                            PokeNavRangeMeters = 751.0156862745098,
                            EncounterRangeMeters = 999.25098039215686,
                            GetMapObjectsMinRefreshSeconds = 16,
                            GetMapObjectsMaxRefreshSeconds = 16,
                            GetMapObjectsMinDistanceMeters = 10.007843017578125f,
                            GoogleMapsApiKey = "AIzaSyDilRVKLXIdPBMsR41VCxx3FpoHpbSEPIc" //change it for you
                        },
                        InventorySettings = new InventorySettings()
                        {
                            MaxPokemon = 1000,
                            MaxBagItems = 1000,
                            BasePokemon = 250,
                            BaseBagItems = 350,
                            BaseEggs = 9
                        },
                        MinimumClientVersion = "0.35.0",
                    };

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(DownloadSettingsResponse)}");
                    return ds.ToByteString();

                case RequestType.DownloadRemoteConfigVersion:
                    DownloadRemoteConfigVersionResponse drcv = new DownloadRemoteConfigVersionResponse();
                    drcv.Result = DownloadRemoteConfigVersionResponse.Types.Result.Success;
                    drcv.ItemTemplatesTimestampMs = 1471650700946;
                    drcv.AssetDigestTimestampMs = 1467338276561000;

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(DownloadRemoteConfigVersionResponse)}");
                    return drcv.ToByteString();

                case RequestType.DownloadItemTemplates:
                    var dit = GlobalSettings.GameMaster.Decode;

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(DownloadItemTemplatesResponse)}");
                    return dit.ToByteString();

                default:
                    throw new Exception($"unknown (Global) Returns type: {typ}");
            }
        }

        public CacheUserData CurrentPlayer
        {
            get
            {
                CacheUserData state;
                WebApiApplication.AuthenticatedUsers.TryGetValue(UEmail, out state);
                return state;
            }
        }

        public bool IsAuth
        {
            get
            {
                if (CurrentPlayer == null)
                    return false;
                else
                    return CurrentPlayer.IsAuthenticated;
            }
        }

        public ByteString GetPacket(RequestType typ, object msg)
        {
            switch (typ)
            {
                case RequestType.SetFavoritePokemon:
                    SetFavoritePokemonResponse sfp = new SetFavoritePokemonResponse();
                    SetFavoritePokemonMessage m = (SetFavoritePokemonMessage)msg;

                    var owned = this.GetPokemonById((ulong)m.PokemonId);
                    if (owned != null)
                    {
                        m.IsFavorite = true;
                        owned.favorite = true;
                        this.Database.OwnedPokemons.Update(owned);
                        sfp.Result = SetFavoritePokemonResponse.Types.Result.Success;
                    }
                    else
                    {
                        sfp.Result = SetFavoritePokemonResponse.Types.Result.ErrorPokemonNotFound;
                    }

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(SetFavoritePokemonResponse)}");
                    return sfp.ToByteString();

                case RequestType.LevelUpRewards:
                    LevelUpRewardsResponse lur = new LevelUpRewardsResponse();
                    lur.Result = LevelUpRewardsResponse.Types.Result.Success;
                    lur.ItemsAwarded.AddRange(new RepeatedField<ItemAward>()
                    {
                        new ItemAward(){ ItemId=ItemId.ItemPokeBall, ItemCount=2},
                        new ItemAward(){ ItemId=ItemId.ItemTroyDisk, ItemCount=2}
                    });
                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(LevelUpRewardsResponse)}");
                    return lur.ToByteString();

                case RequestType.ReleasePokemon:
                    ReleasePokemonResponse rp = this.ReleasePokemon((ReleasePokemonMessage)msg);

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(ReleasePokemonResponse)}");
                    return rp.ToByteString();

                case RequestType.UpgradePokemon:
                    UpgradePokemonResponse up = new UpgradePokemonResponse();
                    //UpgradePokemonMessage upm = (UpgradePokemonMessage)msg;
                    //var uptpkmn = brc.GetPokemonById(upm.PokemonId);
                    //if (uptpkmn!=null)
                    //{
                    //}
                    //else
                    //{
                    //    up.Result = UpgradePokemonResponse.Types.Result.ErrorPokemonNotFound;
                    //}

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(UpgradePokemonResponse)}");
                    return up.ToByteString();

                case RequestType.GetPlayerProfile:
                    GetPlayerProfileResponse gpp = new GetPlayerProfileResponse();
                    gpp.Result = GetPlayerProfileResponse.Types.Result.Success;
                    gpp.StartTime = (long)DateTime.Now.ToUnixTime();
                    gpp.Badges.Add(new POGOProtos.Data.PlayerBadge()
                    {
                        BadgeType = BadgeType.BadgeTravelKm,
                        EndValue = 2674,
                        CurrentValue = 1337
                    });
                    return gpp.ToByteString();

                //case RequestType.SetAvatar:
                //    LevelUpRewardsResponse sa = new LevelUpRewardsResponse();
                //    return sa.ToByteString();

                case RequestType.GetPlayer:

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetPlayerResponse)}");
                    return new GetPlayer().From(this);

                case RequestType.GetInventory:
                    RepeatedField<InventoryItem> items = new RepeatedField<InventoryItem>();
                    //ADD ITEMSS
                    GetInventoryResponse gi = new GetInventoryResponse();
                    gi.Success = true;
                    gi.InventoryDelta = new POGOProtos.Inventory.InventoryDelta()
                    {
                        NewTimestampMs = (long)DateTime.Now.ToUnixTime()
                    };
                    gi.InventoryDelta.InventoryItems.AddRange(items);

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetInventoryResponse)}");
                    return gi.ToByteString();

                case RequestType.GetAssetDigest:
                    var gad = GlobalSettings.GameAssets[this.CurrentPlayer.Platform].Value;

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetAssetDigestResponse)}");
                    return gad.ToByteString();

                //case RequestType.NicknamePokemon:
                //    LevelUpRewardsResponse np = new LevelUpRewardsResponse();
                //    return np.ToByteString();

                case RequestType.GetHatchedEggs:
                    GetHatchedEggsResponse ghe = new GetHatchedEggsResponse();
                    ghe.Success = true;

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetHatchedEggsResponse)}");
                    return ghe.ToByteString();

                case RequestType.CheckAwardedBadges:
                    CheckAwardedBadgesResponse cab = new CheckAwardedBadgesResponse();
                    cab.Success = true;

                    this.Log.Dbg($"TypeOfResponseMessage: {nameof(CheckAwardedBadgesResponse)}");
                    return cab.ToByteString();

                //case RequestType.RecycleInventoryItem:
                //    LevelUpRewardsResponse rii = new LevelUpRewardsResponse();
                //    return rii.ToByteString();

                //case RequestType.ClaimCodename:
                //    LevelUpRewardsResponse cc = new LevelUpRewardsResponse();
                //    return cc.ToByteString();

                default:
                    throw new Exception($"unknown (Player) Returns type: {typ}");
            }
        }

        public string UEmail
        {
            get
            {
                var authInfo = ProtoRequest.AuthInfo;
                if (authInfo.IsNull() || authInfo.Provider.IsNull())
                    throw new Exception("Invalid authentication token! Kicking..");

                JwtSecurityTokenHandler jwth = new JwtSecurityTokenHandler();
                var userJwtToken = jwth.ReadJwtToken(ProtoRequest.AuthInfo.Token.Contents).Payload;
                object userEmail;
                userJwtToken.TryGetValue("email", out userEmail);
                if (userEmail.IsNull())
                    throw new Exception("useremail not found");
                return userEmail.ToString();
            }
        }

        public OwnedPokemon GetPokemonById(ulong id)
        {
            var usr = this.Database.Users.SingleOrDefault(p => p.email == this.UEmail);
            var owned = this.Database.OwnedPokemons.SingleOrDefault(p => p.owner_id == usr.id && (int)id == p.id);
            return owned;
        }

        public ReleasePokemonResponse ReleasePokemon(ReleasePokemonMessage msg)
        {
            var owned = this.GetPokemonById(msg.PokemonId);
            ReleasePokemonResponse rsp = new ReleasePokemonResponse();
            if (owned != null)
            {
                rsp.Result = ReleasePokemonResponse.Types.Result.Success;
                rsp.CandyAwarded = 3;
            }
            else
            {
                rsp.Result = ReleasePokemonResponse.Types.Result.Failed;
            }
            return rsp;
        }
    }
}