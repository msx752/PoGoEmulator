using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            var pkmnTmpl = Extensions.GetPkmnTemplate((byte)p.dexNumber);
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
        }

        public static double CalcCp(this Pokemon p, Player owner)
        {
            var levelSettings = owner.info.GetLevelSettings();
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
            var family = Extensions.GetPkmnFamily((byte)p.dexNumber);
            byte id = (byte)family;
            if (p.isOwned)
            {
                p.owner.candyBag.AddCandy(id, amount);
            }
            /*
                 let family = this.getPkmnFamily(this.dexNumber);
                let id = ENUM.getIdByName(ENUM.POKEMON_FAMILY, family) << 0;
                if (this.owner) this.owner.candyBag.addCandy(id, parseInt(amount));
             */
        }

        public static int HasEvolution(this Pokemon p)
        {
            var pkmnTmpl = Extensions.GetPkmnTemplate((byte)p.dexNumber);
            return (pkmnTmpl.CandyToEvolve << 0);
            /*
                let pkmnTmpl = this.getPkmnTemplate(this.dexNumber);
                return (
                  pkmnTmpl.evolution_ids.length >= 1  );
             */
            //var pkmnTmpl = p.GetPkmnTemplate((byte)p.dexNumber);
        }

        public static bool HasReachedMaxLevel(this Pokemon p)
        {
            return p._level > p.owner.info.GetMaximumLevel() * 2;
        }

        /**
         * @return {Number}
         */
        //candiesToEvolve()
        //      {
        //          let pkmnTmpl = this.getPkmnTemplate(this.dexNumber);
        //          return (pkmnTmpl.candy_to_evolve << 0);
        //      }

        /**
  /**
   * @return {Boolean}
   */
        //hasReachedMaxLevel()
        //      {
        //          return (
        //            this.level > this.owner.info.getMaximumLevel() * 2
        //          );
        //      }
    }
}