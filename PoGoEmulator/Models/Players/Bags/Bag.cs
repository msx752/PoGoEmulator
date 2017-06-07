using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.Bags
{
    public class Bag
    {
        public Bag()
        {
        }

        public int poke_ball { get; set; }
        public int great_ball { get; set; }
        public int ultra_ball { get; set; }
        public int master_ball { get; set; }

        public int potion { get; set; }
        public int super_potion { get; set; }
        public int hyper_potion = 0;
        public int max_potion { get; set; }

        public int revive { get; set; }
        public int max_revive { get; set; }

        public int lucky_egg { get; set; }
        public int troy_disk { get; set; }

        public int incense_ordinary { get; set; }
        public int incense_spicy { get; set; }
        public int incense_cool { get; set; }
        public int incense_floral { get; set; }

        public int razz_berry { get; set; }
        public int bluk_berry { get; set; }
        public int nanab_berry { get; set; }
        public int wepar_berry { get; set; }
        public int pinap_berry { get; set; }

        public int incubator_basic { get; set; }
        public int incubator_basic_unlimited { get; set; }

        public int pokemon_storage_upgrade { get; set; }
        public int storage_upgrade { get; set; }
    }
}