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
            throw new NotImplementedException();
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