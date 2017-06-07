using PoGoEmulator.Models.Worlds.MapObjects;
using POGOProtos.Map;
using POGOProtos.Map.Fort;

namespace PoGoEmulator.Models.Worlds.Forts
{
    public class Fort : MapObject
    {
        public Fort()
        {
            this.type = null;
            this.enabled = true;
            this.deleted = false;
        }

        public FortType? type { get; set; }
        public bool enabled { get; set; }
        public bool deleted { get; set; }

        public Fort(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
        }
    }
}