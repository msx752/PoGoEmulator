using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoGoEmulatorApi.Assets;
using PoGoEmulatorApi.Models;
using POGOProtos.Networking.Responses;

namespace PoGoEmulatorApi
{
    public static class GlobalSettings
    { /// <summary>
      /// server configs </summary>
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
    }
}