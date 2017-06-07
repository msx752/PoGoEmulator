using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.GameMasters;
using POGOProtos.Enums;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models.Pokemons
{
    public static class PokemonExtensions
    {
        public static void CalcStats(this Pokemon p, string owner)
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

        public static PokemonSettings GetPkmnTemplate(this Pokemon p, byte dex)
        {
            var sttng = GlobalSettings.GameMaster.GetPokemonTmplByDex(dex);
            return sttng;
        }

        public static PokemonId GetPkmnName(this Pokemon p, byte dex)
        {
            return (PokemonId)dex;
        }

        public static PokemonFamilyId GetPkmnFamily(this Pokemon p, byte dex)
        {
            return p.GetPkmnTemplate(dex).FamilyId;
        }

        public static void addCandies(this Pokemon p, int amount)
        {
            var family = p.GetPkmnFamily((byte)p.dexNumber);
            /*
                 let family = this.getPkmnFamily(this.dexNumber);
                let id = ENUM.getIdByName(ENUM.POKEMON_FAMILY, family) << 0;
                if (this.owner) this.owner.candyBag.addCandy(id, parseInt(amount));
             */
        }

        public static bool hasEvolution(this Pokemon p)
        {
            /*
                let pkmnTmpl = this.getPkmnTemplate(this.dexNumber);
                return (
                  pkmnTmpl.evolution_ids.length >= 1  );
             */
            //var pkmnTmpl = p.GetPkmnTemplate((byte)p.dexNumber);
            return false;
        }
    }
}