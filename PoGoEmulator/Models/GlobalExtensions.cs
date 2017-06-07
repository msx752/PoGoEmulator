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
    public static class GlobalExtensions
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
        public static ulong GenerateUniqueULongId()
        {
            int count = 2;
            List<int> result = new List<int>(count);

            HashSet<int> candidates = new HashSet<int>();
            for (Int32 top = Int32.MaxValue - count; top < Int32.MaxValue; top++)
            {
                Thread.Sleep(5);
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

        //modification of https://codereview.stackexchange.com/questions/61338/generate-random-numbers-without-repetitions
        public static Int32 GenerateUniqueInt32Id()
        {
            int count = 1;
            List<int> result = new List<int>(count);

            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();
            for (Int32 top = Int32.MaxValue - count; top < Int32.MaxValue; top++)
            {
                Thread.Sleep(5);
                // May strike a duplicate.
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
            return result.First();
        }

        public static PokemonSettings GetPkmnTemplate(byte dex)
        {
            var sttng = GlobalSettings.GameMaster.GetPokemonTmplByDex(dex);
            return sttng;
        }

        public static float GetHalfLevelCpMultiplier(byte lvl)
        {
            var levelSettings = GetLevelSettings();
            var next = levelSettings.CpMultiplier[lvl - 1];

            return (float)Math.Sqrt(Math.Pow(lvl, 2) + ((Math.Pow(next, 2) - Math.Pow(lvl, 2)) / 2));
        }

        public static PlayerLevelSettings GetLevelSettings()
        {
            var pls = GlobalSettings.GameMaster.Settings["PLAYER_LEVEL_SETTINGS"];
            return pls.PlayerLevel;
        }

        public static int GetLevelExp(byte lvl)
        {
            return GlobalExtensions.GetLevelSettings().RequiredExperience[lvl - 1];
        }

        public static int GetMaximumLevel()
        {
            return GlobalExtensions.GetLevelSettings().RequiredExperience.Count;
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