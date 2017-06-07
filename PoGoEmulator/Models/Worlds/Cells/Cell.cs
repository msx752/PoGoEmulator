using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.Worlds.Forts;
using PoGoEmulator.Models.Worlds.MapObjects;
using POGOProtos.Map;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Worlds.Cells
{
    public class Cell : MapObject
    {
        public Cell()
        {
        }

        public ConcurrentDictionary<string, Fort> Forts { get; set; } = new ConcurrentDictionary<string, Fort>();
        public bool synced { get; set; }
        public int expiration { get; set; }

        public Cell(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
        }
    }
}