using System;
using Google.Protobuf;
using PoGoEmulatorApi.Controllers;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Responses;
using POGOProtos.Settings;

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

                    return ds.ToByteString();

                case RequestType.DownloadRemoteConfigVersion:
                    DownloadRemoteConfigVersionResponse drcv = new DownloadRemoteConfigVersionResponse();
                    drcv.Result = DownloadRemoteConfigVersionResponse.Types.Result.Success;
                    drcv.ItemTemplatesTimestampMs = 1471650700946;
                    drcv.AssetDigestTimestampMs = 1467338276561000;
                    return drcv.ToByteString();

                case RequestType.DownloadItemTemplates:
                    var dit = GlobalSettings.GameMaster.Decode;
                    return dit.ToByteString();

                default:
                    throw new Exception($"unknown (Global) Returns type: {typ}");
            }
        }
    }
}