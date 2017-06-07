using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using PoGoEmulator.Models.Worlds.MapObjects;

namespace PoGoEmulator.Models.Worlds.MapObjects
{
    public static class MapObjectExtensions
    {
        //not tested yet
        public static void InitializeMapObject(this MapObject mp, object refClass, object obj)
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

                    case "move_1":
                        pi = refClassType.GetProperty("move1");
                        break;

                    case "move_2":
                        pi = refClassType.GetProperty("move2");
                        break;

                    case "height_m":
                        pi = refClassType.GetProperty("height");
                        break;

                    case "weight_kg":
                        pi = refClassType.GetProperty("weight");
                        break;

                    case "individual_attack":
                        pi = refClassType.GetProperty("ivAttack");
                        break;

                    case "individual_defense":
                        pi = refClassType.GetProperty("ivDefense");
                        break;

                    case "individual_stamina":
                        pi = refClassType.GetProperty("ivStamina");
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
    }
}