using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.Pokemons;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.Partys
{
    public class Party
    {
        public List<Pokemon> party { get; set; }

        public Party()
        {
            party = new List<Pokemon>();
        }
    }
}