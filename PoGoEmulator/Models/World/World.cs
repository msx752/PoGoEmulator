using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POGOProtos.Map;

namespace PoGoEmulator.Models.World
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