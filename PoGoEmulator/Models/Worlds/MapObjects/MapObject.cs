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
            InitializeMapObject(this, obj);
        }

        //not tested yet
        protected void InitializeMapObject(object refClass, object obj)
        {
            Type refClassType = refClass.GetType();
            foreach (var prop in refClassType.GetProperties())
            {
                PropertyInfo pi = null;
                switch (prop.Name.ToLower())
                {
                    case "cooldown":
                        //skip
                        break;

                    case "rewards":// change type to RepeatedItemId and add this case to skip part
                        pi = refClassType.GetProperty("rewards");
                        break;

                    case "gympoints":
                        pi = refClassType.GetProperty("isinbattle");
                        break;

                    case "isinbattle":
                        pi = refClassType.GetProperty("isinbattle");
                        break;

                    case "guardpokemoncp":
                        pi = refClassType.GetProperty("guardPkmnCp");
                        break;

                    case "guardpokemonid":
                        pi = refClassType.GetProperty("guardPkmn");
                        break;

                    case "ownedbyteam":
                        pi = refClassType.GetProperty("team");
                        break;

                    case "cell_id":
                        pi = refClassType.GetProperty("cellId");
                        break;

                    case "min_spawn_expire":
                        pi = refClassType.GetProperty("minExpire");
                        break;

                    case "max_spawn_expire":
                        pi = refClassType.GetProperty("maxExpire");
                        break;

                    case "id":
                        pi = refClassType.GetProperty("uid");
                        break;

                    default:
                        pi = refClassType.GetProperty(prop.Name.ToLower());
                        if (pi == null)
                        {
                            var prp = refClassType.GetProperty("Properties");//or use without reflection, it doesn't matter
                            var mthAdd = prp.PropertyType.GetMethod("Add", new[] { typeof(string), typeof(object) });
                            mthAdd.Invoke(refClassType, new object[] { prop.Name.ToLower(), prop.GetValue(obj) });
                        }
                        break;
                }
                pi?.SetValue(refClassType, prop.GetValue(obj));
            }
        }

        public object this[string key]
        {
            get { return Properties[key]; }
        }

        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}