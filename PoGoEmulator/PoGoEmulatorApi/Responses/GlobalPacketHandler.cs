using System;
using Google.Protobuf;
using PoGoEmulatorApi.Controllers;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Responses;

namespace PoGoEmulatorApi.Responses
{
    public static class GlobalPacketHandler
    {
        public static ByteString GetGlobalPacket(this BaseRpcController brc, RequestType typ, object msg)
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
            throw new Exception($"unknown (Global) Returns type: {typ}");
        }
    }
}