using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulator.Database.Tables;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using POGOProtos.Enums;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator.Responses
{
    public static class Extensions
    {
        public static bool PlayerIsRegistered()
        {
            return false;
        }

        public static void AuthenticatePlayer(this MyHttpContext context)
        {
            context.Response.StatusCode = 53;
            context.Response.RequestId = context.Request.RequestId;
            context.Response.ApiUrl = "pgorelease.nianticlabs.com/custom";
            context.Response.AuthTicket = new AuthTicket()
            {
                ExpireTimestampMs = DateTime.Now.ToUnixTime(new TimeSpan(0, 30, 0)),
            };
            var oauth = context.CachedUserData;
            if (oauth != null)
            {
                oauth.IsAuthenticated = true;
            }
            else
            {
                oauth = new CacheUserData()
                {
                    IsAuthenticated = true
                };
            }
            Global.AuthenticatedUsers.AddOrUpdate(context.UserEmail, oauth, (k, v) => oauth);
        }

        public static User GetPlayerByRequest(this Connection connectedClient)
        {
            var user = connectedClient.Database.Users.SingleOrDefault(p => p.email == connectedClient.HttpContext.UserEmail);
            if (user == null)
            {
                user = new User
                {
                    email = connectedClient.HttpContext.UserEmail,
                    username = connectedClient.HttpContext.UserEmail.Split('@')[0],
                    altitude = connectedClient.HttpContext.Request.Altitude,
                    longitude = connectedClient.HttpContext.Request.Longitude,
                    latitude = connectedClient.HttpContext.Request.Latitude,
                };
                connectedClient.Database.Users.Add(user);
            }
            else
            {
                user.altitude = connectedClient.HttpContext.Request.Altitude;
                user.longitude = connectedClient.HttpContext.Request.Longitude;
                user.latitude = connectedClient.HttpContext.Request.Latitude;
                connectedClient.Database.Users.Update(user);
            }
            return user;
        }

        public static void OnRequest(this Connection connectedClient)
        {
            var context = connectedClient.HttpContext;
            if (!context.IsAuthenticated)
            {
                context.AuthenticatePlayer();
                return;//request ended
            }

            if (connectedClient.HttpContext.CachedUserData.HasSignature == false)//gets device information
            {
                if (connectedClient.HttpContext.Request.Unknown6 != null)
                {
                    //POGOProtos.Networking.Envelopes.Signature
                    //connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature
                    var signature = Encryption.Decrypt(
                           connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature.ToByteArray());

                    var codedStream = new CodedInputStream(signature);
                    var sig = new Signature();
                    sig.MergeFrom(codedStream);
                    if (sig.DeviceInfo != null)
                    {
                        var usrd = connectedClient.HttpContext.CachedUserData;
                        usrd.HasSignature = true;
                        usrd.IsIOS = (sig.DeviceInfo.DeviceBrand == "Apple");
                        bool updtrslt = Global.AuthenticatedUsers.TryUpdate(connectedClient.HttpContext.UserEmail, usrd,
                             connectedClient.HttpContext.CachedUserData);
                        if (!updtrslt)
                        {
                            throw new Exception(" CONCURRENT ACCESS ERROR this shouldn't happen");
                        }
#if DEBUG
                        var devc = usrd.IsIOS ? "Apple" : "Android";
                        Logger.Write($"User:{connectedClient.HttpContext.UserEmail} connected with {devc} device.", Enums.LogLevel.Success);
#endif
                    }
                }
            }

            if (!context.Request.Requests.Any())
            {
                if (context.Request.Unknown6 != null && context.Request.Unknown6.RequestType == 6)
                {
                    connectedClient.EnvelopResponse();
                    return;
                }
                else
                {
                    throw new Exception("Invalid Request!.");
                }
            }
            RepeatedField<ByteString> body = new RepeatedField<ByteString>();
            foreach (var req in context.Request.Requests)
            {
                body.Add(connectedClient.ProcessResponse(req));
            }

            connectedClient.EnvelopResponse(body);
        }

        public static ByteString ProcessResponse(this Connection connectedClient, Request req)
        {
            var type = req.RequestType;
            CodedInputStream codedStream = new CodedInputStream(req.RequestMessage.ToByteArray());

            var strType = $"POGOProtos.Networking.Requests.Messages.{type}Message";
            object msg = Activator.CreateInstance(FindType(strType));
            MethodInfo methodMergeFrom = msg?.GetType()
                .GetMethods()
                .ToList()
                .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");
            methodMergeFrom.Invoke(msg, new object[] { codedStream });

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
                    return connectedClient.GetPacket(type, msg);

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
                    return connectedClient.GetGlobalPacket(type, msg);

                default:
                    throw new Exception($"unknown request Type:{type}");
                    break;
            }
        }

        public static ByteString GetGlobalPacket(this Connection connectedClient, RequestType typ, object msg)
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
                    return c.ToByteString();

                    //case RequestType.GetDownloadUrls:
                    //    GetDownloadUrlsResponse gdu = new GetDownloadUrlsResponse();
                    //    return gdu.ToByteString();

                    //case RequestType.DownloadSettings:
                    //    DownloadSettingsResponse ds = new DownloadSettingsResponse();
                    //    return ds.ToByteString();

                    //case RequestType.DownloadRemoteConfigVersion:
                    //    DownloadRemoteConfigVersionResponse drcv = new DownloadRemoteConfigVersionResponse();
                    //    return drcv.ToByteString();

                    //case RequestType.DownloadItemTemplates:
                    //    DownloadItemTemplatesResponse dit = new DownloadItemTemplatesResponse();
                    //    return dit.ToByteString();
            }
            throw new Exception("unknown (Global) Returns type");
        }

        public static ByteString GetPacket(this Connection connectedClient, RequestType typ, object msg)
        {
            switch (typ)
            {
                //case RequestType.SetFavoritePokemon:
                //    SetFavoritePokemonResponse sfp = new SetFavoritePokemonResponse();
                //    return sfp.ToByteString();

                //case RequestType.LevelUpRewards:
                //    LevelUpRewardsResponse lur = new LevelUpRewardsResponse();
                //    return lur.ToByteString();

                //case RequestType.ReleasePokemon:
                //    ReleasePokemonResponse rp = new ReleasePokemonResponse();
                //    return rp.ToByteString();

                //case RequestType.UpgradePokemon:
                //    UpgradePokemonResponse up = new UpgradePokemonResponse();
                //    return up.ToByteString();

                //case RequestType.GetPlayerProfile:
                //    LevelUpRewardsResponse gpp = new LevelUpRewardsResponse();
                //    return gpp.ToByteString();

                //case RequestType.SetAvatar:
                //    LevelUpRewardsResponse sa = new LevelUpRewardsResponse();
                //    return sa.ToByteString();

                case RequestType.GetPlayer:
                    User usr =
                        connectedClient.Database.Users.SingleOrDefault(
                            p => p.email == connectedClient.HttpContext.UserEmail);
                    GetPlayerResponse gpr = new GetPlayerResponse();
                    gpr.Success = true;
                    //update with database
                    gpr.PlayerData = new POGOProtos.Data.PlayerData()
                    {
                        CreationTimestampMs = (long)DateTime.Now.ToUnixTime(new TimeSpan()),
                        Username = usr.username,
                        Team = (TeamColor)usr.team,
                        Avatar = new POGOProtos.Data.Player.PlayerAvatar()
                        {
                            Skin = 1,
                            Hair = 1,
                            Shirt = 1,
                            Pants = 1,
                            Eyes = 1,
                            Backpack = 1,
                            Hat = 1,
                            Shoes = 1
                        },
                        MaxPokemonStorage = 250,
                        MaxItemStorage = 350,
                        ContactSettings = new POGOProtos.Data.Player.ContactSettings()
                        {
                            SendMarketingEmails = usr.send_marketing_emails == 1,
                            SendPushNotifications = usr.send_push_notifications == 1
                        },
                        RemainingCodenameClaims = 10,
                    };
                    gpr.PlayerData.TutorialState.AddRange(new List<TutorialState>()
                    {
                        (TutorialState)1,
                        (TutorialState)0,
                        (TutorialState)3,
                        (TutorialState)4,
                        (TutorialState)7
                    });
                    return gpr.ToByteString();

                    //case RequestType.GetInventory:
                    //    LevelUpRewardsResponse gi = new LevelUpRewardsResponse();
                    //    return gi.ToByteString();

                    //case RequestType.GetAssetDigest:
                    //    LevelUpRewardsResponse gad = new LevelUpRewardsResponse();
                    //    return gad.ToByteString();

                    //case RequestType.NicknamePokemon:
                    //    LevelUpRewardsResponse np = new LevelUpRewardsResponse();
                    //    return np.ToByteString();

                    //case RequestType.GetHatchedEggs:
                    //    LevelUpRewardsResponse ghe = new LevelUpRewardsResponse();
                    //    return ghe.ToByteString();

                    //case RequestType.CheckAwardedBadges:
                    //    LevelUpRewardsResponse cab = new LevelUpRewardsResponse();
                    //    return cab.ToByteString();

                    //case RequestType.RecycleInventoryItem:
                    //    LevelUpRewardsResponse rii = new LevelUpRewardsResponse();
                    //    return rii.ToByteString();

                    //case RequestType.ClaimCodename:
                    //    LevelUpRewardsResponse cc = new LevelUpRewardsResponse();
                    //    return cc.ToByteString();
            }
            throw new Exception("unknown Returns type");
        }

        public static void EnvelopResponse(this Connection connectedClient, RepeatedField<ByteString> returns = null)
        {
            if (returns != null)
                connectedClient.HttpContext.Response.Returns.AddRange(returns);

            if (connectedClient.HttpContext.Request.AuthTicket == null)
                connectedClient.HttpContext.Response.AuthTicket = new AuthTicket() { };

            connectedClient.HttpContext.Response.Unknown6.Add(new Unknown6Response()
            {
                ResponseType = 6,
                Unknown2 = new Unknown6Response.Types.Unknown2()
                {
                    Unknown1 = 1
                }
            });
            connectedClient.HttpContext.Response.StatusCode = 1;
        }

        public static Type FindType(string qualifiedTypeName)
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
    }
}