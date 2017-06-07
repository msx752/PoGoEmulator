using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.Infos
{
    public class Info
    {
        public Info()
        {
            pokeballsThrown = 3;
            pokeStopVisits = 2;
            pkmnCaptured = 101;
            uniquePokedexEntries = 100;
            pkmnEncountered = 151;
            maxItemStorage = 350;
            maxPkmnStorage = 250;
            levelReward = false;
        }

        public int stardust { get; set; }
        public int pokecoins { get; set; }
        public int _exp { get; set; }
        public int _team { get; set; }
        public int _level { get; set; }
        public int prevLvlExp { get; set; }
        public int nextLvlExp { get; set; }
        public int kmWalked { get; set; }
        public int bigMagikarpCaught { get; set; }
        public int pkmnDeployed { get; set; }
        public int LuckyEggExp { get; set; }
        public bool levelReward { get; set; }
        public int maxPkmnStorage { get; set; }
        public int maxItemStorage { get; set; }
        public int pkmnEncountered { get; set; }
        public int uniquePokedexEntries { get; set; }
        public int pkmnCaptured { get; set; }
        public int pokeStopVisits { get; set; }
        public int pokeballsThrown { get; set; }
        public int eggsHatched { get; set; }
    }
}