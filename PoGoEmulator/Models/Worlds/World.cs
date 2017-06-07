using System.Collections.Concurrent;
using PoGoEmulator.Controllers.Layers;
using PoGoEmulator.Database;
using PoGoEmulator.Models.Players;
using POGOProtos.Map;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Worlds
{
    //THIS WILL BE DEPREATE
    public class World
    {
        static World()
        {
            Cells = new ConcurrentDictionary<string, MapCell>();
            Players = new ConcurrentDictionary<string, Player>();
        }

        public static ConcurrentDictionary<string, MapCell> Cells { get; set; }
        public static ConcurrentDictionary<string, Player> Players { get; set; }

        public FunctionLayer Layer { get; set; }

        public World(FunctionLayer _layer)
        {
            this.Layer = _layer;
        }
    }
}