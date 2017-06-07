using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PoGoEmulator.Models.Players.Currencys
{
    public class Currency
    {
        public Player Player { get; set; }

        public Currency(Player p)
        {
            Player = p;
        }

        public Dictionary<string, int> Serialize()
        {
            Dictionary<string, int> dat = new Dictionary<string, int>();
            dat.Add("POKECOIN", Player.info.pokecoins);
            dat.Add("STARDUST", Player.info.stardust);
            return dat;
        }
    }
}