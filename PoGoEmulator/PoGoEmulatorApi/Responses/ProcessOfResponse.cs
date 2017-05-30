using System;
using System.Linq;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Controllers;
using POGOProtos.Networking.Requests;

namespace PoGoEmulatorApi.Responses
{
    public static class ProcessOfResponse
    {
        public static ByteString ProcessResponse(this BaseRpcController brcontroller, Request req)
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
                        return brcontroller.GetPacket(type, msg);

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
                        return brcontroller.GetGlobalPacket(type, msg);

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

        public static RepeatedField<ByteString> ProcessRequests(this BaseRpcController brcontroller)
        {
            RepeatedField<ByteString> Body = new RepeatedField<ByteString>();
            foreach (var req in brcontroller.ProtoRequest.Requests)
            {
                Body.Add(brcontroller.ProcessResponse(req));
            }
            return Body;
        }
    }
}