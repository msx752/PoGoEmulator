using PoGoPrivate.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace PoGoPrivate
{
    public static class Assets
    {
        public static string[] Plaforms = new string[] { "android", "ios" };
        public static Dictionary<string, object> GameAssets = new Dictionary<string, object>();
        public static Dictionary<string, object> GameMaster = new Dictionary<string, object>();

        public static void ValidateModels()
        {
#if DEBUG

            Logger.Write("Validating Models...", Enums.LogLevel.Debug);
#endif
            if (!File.Exists(Path.Combine(Global.Cfg.DUMP_ASSET_PATH, "game_master")))
            {
                Logger.Write("game_master not found ", Enums.LogLevel.Error);
                throw new Exception("file not found");
            }
            else
            {
                Logger.Write("Should be validated in here...", Enums.LogLevel.Debug);
            }
        }
    }
}