using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Database;
using PoGoEmulator.Models.GameMasters;
using PoGoEmulator.Models.Players;
using PoGoEmulator.Models.Players.CandyBags;
using PoGoEmulator.Models.Players.Infos;
using POGOProtos.Enums;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models.Pokemons
{
    public static class PokemonExtensions
    {
        public static void CalcStats(this Pokemon p, Player owner)
        {
            var pkmnTmpl = p.GetPokemonSettings();
            var stats = pkmnTmpl.Stats;
            var minIV = GlobalSettings.GameCfg.PokemonSettings.MIN_IV;
            var maxIV = GlobalSettings.GameCfg.PokemonSettings.MAX_IV;
            p.attack = stats.BaseAttack;
            p.defense = stats.BaseDefense;
            p.stamina = stats.BaseStamina;
            p.staminaMax = p.stamina;

            p.ivAttack = ~~(GlobalSettings.Random.Next() * maxIV) + minIV;
            p.ivDefense = ~~(GlobalSettings.Random.Next() * maxIV) + minIV;
            p.ivStamina = ~~(GlobalSettings.Random.Next() * maxIV) + minIV;

            p.height = (int)(pkmnTmpl.PokedexHeightM + ((GlobalSettings.Random.Next() * pkmnTmpl.HeightStdDev) + .1));
            p.weight = (int)(pkmnTmpl.PokedexWeightKg + ((GlobalSettings.Random.Next() * pkmnTmpl.PokedexHeightM) + .1));

            if (owner != null)
            {
                p.cp = (int)(Math.Floor((GlobalSettings.Random.Next() * p.CalcCp(owner)) + 16));
            }
            p.CalcMoves();
        }

        public static PokemonSettings GetPokemonSettings(this Pokemon p)
        {
            return GlobalExtensions.GetPkmnTemplate((byte)p.dexNumber);
        }

        public static void CalcMoves(this Pokemon p)
        {
            var pkmnTmpl = p.GetPokemonSettings();

            var weakMoves = pkmnTmpl.QuickMoves;
            var strongMoves = pkmnTmpl.CinematicMoves;

            p.move1 = (int)weakMoves[(GlobalSettings.Random.Next() * weakMoves.Count) << 0];
            p.move2 = (int)strongMoves[(GlobalSettings.Random.Next() * strongMoves.Count) << 0];
        }

        public static double CalcCp(this Pokemon p, Player owner)
        {
            var levelSettings = GlobalExtensions.GetLevelSettings();
            var ecpm = levelSettings.CpMultiplier[owner.info._level - 1];
            var atk = (p.attack + p.ivAttack) * ecpm;
            var def = (p.defense + p.ivDefense) * ecpm;
            var sta = (p.stamina + p.ivStamina) * ecpm;

            return Math.Max(10, Math.Floor(Math.Sqrt(atk * atk * def * sta) / 10));
        }

        public static void SetFavorite(this Pokemon p, bool truth)
        {
            p.favorite = truth ? 1 : 0;
        }

        public static void SetFavorite(this Pokemon p, string name)
        {
            p.nickname = name;
        }

        public static void AddCandies(this Pokemon p, int amount)
        {
            var family = p.GetPokemonFamilyId();
            byte id = (byte)family;
            if (p.isOwned)
                p.owner.candyBag.AddCandy(id, amount);
        }

        public static PokemonFamilyId GetPokemonFamilyId(this Pokemon p)
        {
            return GlobalExtensions.GetPkmnFamily((byte)p.dexNumber);
        }

        public static bool HasEvolution(this Pokemon p)
        {
            var pkmnTmpl = p.GetPokemonSettings();
            return pkmnTmpl.EvolutionIds.Count >= 1;
        }

        public static int CandiesToEvolve(this Pokemon p)
        {
            var pkmnTmpl = p.GetPokemonSettings();
            return pkmnTmpl.CandyToEvolve << 0;
        }

        public static bool HasReachedMaxLevel(this Pokemon p)
        {
            return p._level > GlobalExtensions.GetMaximumLevel() * 2;
        }

        public static void InsertIntoDatabase(this Pokemon p, PoGoDbContext db)
        {
        }

        public static void UpdateDatabase(this Pokemon p, PoGoDbContext db)
        {
        }

        public static void DeleteFromDatabase(this Pokemon p, PoGoDbContext db)
        {
        }
    }
}