using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.Worlds.MapObjects;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Pokemons.WildPokemons
{
    public class WildPokemon : Pokemon
    {
        public WildPokemon()
        {
            hasSeen = new List<Seen>();
            hasCatched = new List<string>();
            this.uid = GlobalExtensions.GenerateUniqueInt32Id();
            despawnIn = -1;
            creation = DateTime.Now;
            Random r = new Random();
            this.expiration = (Math.Floor((r.NextDouble() * this.maxExpire) + this.minExpire) * 1e3);
        }

        public int despawnIn { get; set; }
        public bool isDespawned { get; set; }
        public int minExpire { get; set; }
        public int maxExpire { get; set; }
        public DateTime creation { get; set; }
        public double expiration { get; set; }
        public List<string> hasCatched { get; set; }
        public List<Seen> hasSeen { get; set; }

        public WildPokemon(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
        }
    }

    public class Seen
    {
        public int cp { get; set; }
        public string uid { get; set; }
    }
}