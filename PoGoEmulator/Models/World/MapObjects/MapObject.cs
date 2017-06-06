using System.Collections.Generic;
using System.Reflection;
using POGOProtos.Map;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.World.MapObjects
{
    public class MapObject
    {
        public string uid { get; set; }

        public string cellId { get; set; }

        public double altitude { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public MapObject(object obj)
        {
            Properties = new Dictionary<string, object>();
            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                PropertyInfo pi = null;
                switch (prop.Name.ToLower())
                {
                    case "cell_id":
                        pi = this.GetType().GetProperty("cellId");
                        break;

                    case "min_spawn_expire":
                        pi = this.GetType().GetProperty("minExpire");
                        break;

                    case "max_spawn_expire":
                        pi = this.GetType().GetProperty("maxExpire");
                        break;

                    case "id":
                        pi = this.GetType().GetProperty("uid");
                        break;

                    default:
                        Properties[prop.Name.ToLower()] = prop.GetValue(obj);
                        break;
                }
                pi?.SetValue(this, prop.GetValue(obj));
            }
        }

        public Dictionary<string, object> Properties { get; set; }
    }
}