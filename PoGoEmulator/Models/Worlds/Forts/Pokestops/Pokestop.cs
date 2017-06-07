using PoGoEmulator.Models.Worlds.MapObjects;
using POGOProtos.Map;
using POGOProtos.Map.Fort;

namespace PoGoEmulator.Models.Worlds.Forts.Pokestops
{
    public class Pokestop : Worlds.Forts.Fort
    {
        public string name { get; set; }

        public string description { get; set; }

        public string img_url { get; set; }

        public int experience { get; set; }

        public string rewards { get; set; }

        public double cooldown { get; set; }

        public Pokestop()
        {
            cooldown = 10e3;
        }

        public Pokestop(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
        }
    }
}