using System;
using System.Collections.Generic;
using System.Reflection;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Worlds.MapObjects
{
    public class MapObject
    {
        public string uid { get; set; }

        public string cellId { get; set; }

        public double altitude { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public MapObject()
        {
        }

        public MapObject(object obj) : this()
        {
            this.InitializeMapObject(this, obj);
        }

        public object this[string key]
        {
            get { return Properties[key]; }
        }

        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}