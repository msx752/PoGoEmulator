using System.Collections.Concurrent;
using POGOProtos.Map;

namespace PoGoEmulator.Models.Worlds
{
    //THIS WILL BE DEPREATE
    public class World
    {
        static World()
        {
            Cells = new ConcurrentDictionary<string, MapCell>();
        }

        public static ConcurrentDictionary<string, MapCell> Cells { get; set; }

        public World()
        {
        }
    }
}