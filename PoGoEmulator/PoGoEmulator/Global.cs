using PoGoEmulator.Models;
using System;
using System.Collections.Generic;
using System.Net;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator
{
    public static class Global
    {
        public static Configs Cfg = new Configs()
        {
#if DEBUG
            RequestTimeout = new TimeSpan(0, 10, 0)
#endif
        };

        public static Dictionary<string, KeyValuePair<byte[], GetAssetDigestResponse>> GameAssets =
            new Dictionary<string, KeyValuePair<byte[], GetAssetDigestResponse>>();

        public static GameMaster GameMaster = null;
    }
}