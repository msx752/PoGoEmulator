using POGOProtos.Enums;
using POGOProtos.Map.Fort;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Worlds.Forts.Gyms
{
    public class Gym : Fort
    {
        public Gym()
        {
        }

        public TeamColor team { get; set; }
        public PokemonId guardPkmn { get; set; }
        public int guardPkmnCp { get; set; }
        public long gympoints { get; set; }
        public bool isinbattle { get; set; }

        public Gym(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
        }
    }
}