using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.GameMasters;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models.Players.CandyBags
{
    public static class CandyBagExtensions
    {
        public static PokemonSettings GetPkmnTemplate(this CandyBag c, byte dex)
        {
            PokemonSettings ps = GlobalSettings.GameMaster.GetPokemonTmplByDex(dex);

            return ps;
        }
    }
}