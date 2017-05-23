using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoGoPrivate.Logging;

namespace PoGoPrivate.Models
{
    public static class Assets
    {
        public static void ValidateModels()
        {
#if DEBUG

            Logger.Write("Validating Models...", Enums.LogLevel.Debug);
#endif
            if (!File.Exists(Path.Combine(Global.cfg.DUMP_ASSET_PATH, "game_master")))
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