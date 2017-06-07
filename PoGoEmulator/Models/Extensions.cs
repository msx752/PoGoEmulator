using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PoGoEmulator.Models.GameMasters;
using PoGoEmulator.Models.Pokemons;
using POGOProtos.Enums;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models
{
    public static class Extensions
    {
        public static bool ValidUsername(string name)
        {
            var rx_username = "/[^a-z\\d]/i";
            var r = Regex.Match(name, rx_username);
            return !r.Success;
        }

        public static bool ValidEmail(string email)
        {
            var rx_username = "/\\S+@\\S+\\.\\S+/";
            var r = Regex.Match(email, rx_username);
            return r.Success;
        }

        public static string IdToPkmnBundleName(int index)
        {
            return "pm" + (index >= 10 ? index >= 100 ? "0" : "00" : "000") + index;
        }

        public static double RandomRequestId()
        {
            Random r = new Random();
            return 1e18 - Math.Floor(r.NextDouble() * 1e18);
        }

        //modification of https://codereview.stackexchange.com/questions/61338/generate-random-numbers-without-repetitions
        public static ulong GenerateUniqueId()
        {
            int count = 2;
            List<int> result = new List<int>(count);

            HashSet<int> candidates = new HashSet<int>();
            for (Int32 top = Int32.MaxValue - count; top < Int32.MaxValue; top++)
            {
                int value = GlobalSettings.Random.Next(top + 1);
                if (candidates.Add(value))
                {
                    result.Add(value);
                }
                else
                {
                    result.Add(top);
                    candidates.Add(top);
                }
            }
            var str = (ulong)(((result[0] - 23) * 3) + ((result[1] + 3) * 23));
            var lent = str.ToString().Length;
            if (lent < 20)
            {
                for (int i = 0; i < 20; i++)
                {
                    str *= 10;
                    if (str.ToString().Length >= 20)//ulong fixed
                    {
                        str -= (ulong)i;
                        break;
                    }
                }
            }
            return (ulong)str;
        }

        public static PokemonSettings GetPkmnTemplate(byte dex)
        {
            var sttng = GlobalSettings.GameMaster.GetPokemonTmplByDex(dex);
            return sttng;
        }

        public static PokemonId GetPkmnName(byte dex)
        {
            return (PokemonId)dex;
        }

        public static PokemonFamilyId GetPkmnFamily(byte dex)
        {
            return GetPkmnTemplate(dex).FamilyId;
        }
    }
}