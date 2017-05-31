using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulator.Database.Tables;
using PoGoEmulator.Enums;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using POGOProtos.Inventory;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using POGOProtos.Settings;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace

namespace PoGoEmulator.Models
{
    public class FuncConnection : AuthConnection
    {
        public FuncConnection(TcpClient client, CancellationTokenSource cancelToken) : base(client, cancelToken)
        {
        }

        public virtual HttpStreamResult OnRequest()
        {
            if (!IsAuth)
            {
                return base.AuthenticatePlayer();
            }
            else
            {
                AddOrUpdateUserLocation();
            }

            //Log.Debug($"HasSignature:{CurrentPlayer.HasSignature}");
            if (CurrentPlayer.HasSignature == false)
            {
                if (ProtoResponse.Unknown6 != null)
                {
                    //POGOProtos.Networking.Envelopes.Signature
                    //connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature
                    var signatur = Encryption.Decrypt(
                           StreamContext.ProtoRequest.Unknown6.Unknown2.EncryptedSignature.ToByteArray());
                    var codedStream = new CodedInputStream(signatur);
                    var sig = new Signature();
                    sig.MergeFrom(codedStream);
                    if (sig.DeviceInfo != null)
                    {
                        var usrd = CurrentPlayer;
                        usrd.HasSignature = true;
                        usrd.IsIOS = (sig.DeviceInfo.DeviceBrand == "Apple");
                        bool updtrslt = UpdateCurrentPlayer(usrd);
                        if (!updtrslt)
                        {
                            throw new Exception(" CONCURRENT ACCESS ERROR this shouldn't happen");
                        }
                    }
                }
            }

            //Log.Debug($"HasSignature:{CurrentPlayer.HasSignature}, Platform:{CurrentPlayer.Platform}");
            //Log.Debug($"ProtoRequest.Requests.Count:{ProtoRequest.Requests.Count}");
            if (StreamContext.ProtoRequest.Requests.Count == 0)
            {
                if (StreamContext.ProtoRequest.Unknown6 != null && StreamContext.ProtoRequest.Unknown6.RequestType == 6)
                {
                    //Log.Debug($"ProtoRequest.Unknown6.RequestType:{ProtoRequest.Unknown6.RequestType}");
                    return base.EnvelopResponse();
                }
                else
                {
                    throw new Exception("Invalid Request!.");
                }
            }
            RepeatedField<ByteString> requests = this.GetPlyerRequests();
            return EnvelopResponse(requests);
        }

        private bool UpdateCurrentPlayer(CacheUserData updatedData)
        {
            return Global.AuthenticatedUsers.AddOrUpdate(UEmail, updatedData, (k, v) => updatedData) != null;
        }

        protected RepeatedField<ByteString> GetPlyerRequests()
        {
            RepeatedField<ByteString> Body = new RepeatedField<ByteString>();

            //this.Log.Dbg($"brcontroller.ProtoRequest.Requests.Count: {this.ProtoRequest.Requests.Count}");
            foreach (var req in this.StreamContext.ProtoRequest.Requests)
            {
                var r = this.ProcessResponse(req);
                Body.Add(r);
            }
            return Body;
        }

        protected ByteString ProcessResponse(Request req)
        {
            try
            {
                var type = req.RequestType;
                CodedInputStream codedStream = new CodedInputStream(req.RequestMessage.ToByteArray());
                var strType = $"POGOProtos.Networking.Requests.Messages.{type}Message";
                object msg = Activator.CreateInstance(Extensions.FindTypeOfObject(strType));
                MethodInfo methodMergeFrom = msg?.GetType()
                    .GetMethods()
                    .ToList()
                    .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");

                methodMergeFrom.Invoke(msg, new object[] { codedStream });

                //this.Log.Dbg($"TypeOfRequestMessage: {strType}");
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
                        return this.GetPlayerPacket(type, msg);

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

        public override HttpStreamResult Abort(RequestState state, Exception e = null)
        {
            return base.Abort(state, e);
        }

        protected ByteString GetGlobalPacket(RequestType typ, object msg)
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

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(CheckChallengeResponse)}");
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

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(DownloadSettingsResponse)}");
                    return ds.ToByteString();

                case RequestType.DownloadRemoteConfigVersion:
                    DownloadRemoteConfigVersionResponse drcv = new DownloadRemoteConfigVersionResponse();
                    drcv.Result = DownloadRemoteConfigVersionResponse.Types.Result.Success;
                    drcv.ItemTemplatesTimestampMs = 1471650700946;
                    drcv.AssetDigestTimestampMs = 1467338276561000;

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(DownloadRemoteConfigVersionResponse)}");
                    return drcv.ToByteString();

                case RequestType.DownloadItemTemplates:
                    var dit = Global.GameMaster.Decode;

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(DownloadItemTemplatesResponse)}");
                    return dit.ToByteString();

                default:
                    throw new Exception($"unknown (Global) Returns type: {typ}");
            }
        }

        protected ByteString GetPlayerPacket(RequestType typ, object msg)
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

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(SetFavoritePokemonResponse)}");
                    return sfp.ToByteString();

                case RequestType.LevelUpRewards:
                    LevelUpRewardsResponse lur = new LevelUpRewardsResponse();
                    lur.Result = LevelUpRewardsResponse.Types.Result.Success;
                    lur.ItemsAwarded.AddRange(new RepeatedField<ItemAward>()
                    {
                        new ItemAward(){ ItemId=ItemId.ItemPokeBall, ItemCount=2},
                        new ItemAward(){ ItemId=ItemId.ItemTroyDisk, ItemCount=2}
                    });
                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(LevelUpRewardsResponse)}");
                    return lur.ToByteString();

                case RequestType.ReleasePokemon:
                    ReleasePokemonResponse rp = this.ReleasePokemon((ReleasePokemonMessage)msg);

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(ReleasePokemonResponse)}");
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

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(UpgradePokemonResponse)}");
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

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetPlayerResponse)}");
                    return this.GetPlayer();

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

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetInventoryResponse)}");
                    return gi.ToByteString();

                case RequestType.GetAssetDigest:
                    var gad = Global.GameAssets[this.CurrentPlayer.Platform].Value;

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetAssetDigestResponse)}");
                    return gad.ToByteString();

                //case RequestType.NicknamePokemon:
                //    LevelUpRewardsResponse np = new LevelUpRewardsResponse();
                //    return np.ToByteString();

                case RequestType.GetHatchedEggs:
                    GetHatchedEggsResponse ghe = new GetHatchedEggsResponse();
                    ghe.Success = true;

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(GetHatchedEggsResponse)}");
                    return ghe.ToByteString();

                case RequestType.CheckAwardedBadges:
                    CheckAwardedBadgesResponse cab = new CheckAwardedBadgesResponse();
                    cab.Success = true;

                    //this.Log.Dbg($"TypeOfResponseMessage: {nameof(CheckAwardedBadgesResponse)}");
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

        protected ByteString GetPlayer()
        {
            User usr =
                this.Database.Users.FirstOrDefault(
                    p => p.email == this.UEmail);

            var gpr = new GetPlayerResponse();
            gpr.Success = true;
            //update with database
            gpr.PlayerData = new POGOProtos.Data.PlayerData()
            {
                CreationTimestampMs = (long)DateTime.Now.ToUnixTime(),
                Username = usr.username,
                Avatar = new POGOProtos.Data.Player.PlayerAvatar()
                {
                },
                MaxPokemonStorage = 250,
                MaxItemStorage = 350,
                ContactSettings = new POGOProtos.Data.Player.ContactSettings(),
                RemainingCodenameClaims = 10,
                DailyBonus = new POGOProtos.Data.Player.DailyBonus(),
                EquippedBadge = new POGOProtos.Data.Player.EquippedBadge(),
            };
            gpr.PlayerData.Currencies.AddRange(new List<Currency>()
            {
                new Currency(){ Name="POKECOIN"},
                new Currency(){ Name="STARDUST"}
            });
            gpr.PlayerData.TutorialState.AddRange(new List<TutorialState>()
            {
              TutorialState.LegalScreen,
              TutorialState.AvatarSelection,
              TutorialState.PokemonCapture,
              TutorialState.NameSelection,
              TutorialState.FirstTimeExperienceComplete
            });
            return gpr.ToByteString();
        }

        protected OwnedPokemon GetPokemonById(ulong id)
        {
            var usr = this.Database.Users.SingleOrDefault(p => p.email == this.UEmail);
            var owned = this.Database.OwnedPokemons.SingleOrDefault(p => p.owner_id == usr.id && (int)id == p.id);
            return owned;
        }

        protected ReleasePokemonResponse ReleasePokemon(ReleasePokemonMessage msg)
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