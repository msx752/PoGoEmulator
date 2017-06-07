using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.Players.Infos;

namespace PoGoEmulator.Models.Players
{
    public static class PlayerExtensions
    {
        public static int CalcCp(this Player p)
        {
            var levelSettings = p.info.GetLevelSettings();
            return 0;
        }
    }
}