using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using PoGoEmulator.Assets;
using PoGoEmulator.Models;
using POGOProtos.Map;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator
{
    public static class GlobalSettings
    {
        /// <summary>
        /// server configs 
        /// </summary>
        public static Configs Cfg = new Configs()
        {
            //#if DEBUG
            //            RequestTimeout = new TimeSpan(0, 10, 0)
            //#endif
        };

        /// <summary>
        /// static datas 
        /// </summary>
        public static Dictionary<string, KeyValuePair<byte[], GetAssetDigestResponse>> GameAssets =
            new Dictionary<string, KeyValuePair<byte[], GetAssetDigestResponse>>();

        /// <summary>
        /// static datas 
        /// </summary>
        public static GameMaster GameMaster = null;

        public static ConcurrentDictionary<string, CacheUserData> AuthenticatedUsers { get; set; } =
            new ConcurrentDictionary<string, CacheUserData>();//it must be re-configure in the future because user must be logout in somewhere (do i need store authTicket too ?)

        public static ConcurrentDictionary<ulong, MapCell> MapCells { get; set; } =
            new ConcurrentDictionary<ulong, MapCell>();
    }
}