using PoGoEmulator.Models.World.MapObjects;
using POGOProtos.Map;
using POGOProtos.Map.Fort;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.World.SpawnPoints
{
    public class SpawnPoint : MapObject
    {
        public FortType type { get; set; }
        public int range { get; set; }

        public int minExpire { get; set; }
        public int maxExpire { get; set; }
        public bool isSpawn { get; set; }

        public object spawns { get; set; }
        public object activeSpawns { get; set; }

        public SpawnPoint(object obj) : base(obj)
        {
            range = 3;
            isSpawn = true;
            this.uid += type.ToString().ToUpper()[0];
        }
    }
}