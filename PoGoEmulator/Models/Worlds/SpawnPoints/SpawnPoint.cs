using PoGoEmulator.Models.Worlds.MapObjects;
using POGOProtos.Map.Fort;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Worlds.SpawnPoints
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

        public SpawnPoint()
        {
            range = 3;
            isSpawn = true;
        }

        public SpawnPoint(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
            this.uid += type.ToString().ToUpper()[0];
        }
    }
}