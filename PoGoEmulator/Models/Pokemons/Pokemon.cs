using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.Worlds.MapObjects;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Pokemons
{
    public class Pokemon : MapObject
    {
        public Pokemon()
        {
            Random r = new Random();
            cpMultiplier = r.NextDouble() + 1.0;
        }

        public Pokemon(object obj) : this()
        {
            this.InitializeMapObject(this, obj);

            if (this.isWild && !this.isOwned) this.CalcStats(this.owner);
        }

        public double cpMultiplier { get; set; }
        public int dexNumber { get; set; }
        public int capturedLevel { get; set; }
        public int cp { get; set; }
        public int addCpMultiplier { get; set; }
        public int move1 { get; set; }
        public int move2 { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int stamina { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public int ivAttack { get; set; }
        public int ivDefense { get; set; }
        public int ivStamina { get; set; }
        public int staminaMax { get; set; }
        public int favorite { get; set; }
        public string owner { get; set; }
        public string nickname { get; set; }
        public string pokeball { get; set; }
        public string spawnPoint { get; set; }
        public bool isWild { get; set; }
        public bool isOwned { get; set; }
        public int _level { get; set; } = 1;
    }
}