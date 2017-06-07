using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.PokeDexs
{
    public class PokeDex
    {
        public Poke[] pkmns { get; set; }
            = new Poke[GlobalSettings.ServerCfg.MAX_POKEMON_NATIONAL_ID + 1];

        public PokeDex()
        {
        }

        public object Serialize()
        {
            //let out = [];
            //for (let key in this.pkmns)
            //{
            //    let pkmn = this.pkmns[key];
            //      out.push({
            //        modified_timestamp_ms: +new Date() - 1e3,
            //                  inventory_item_data:
            //        {
            //            pokedex_entry:
            //            {
            //                pokemon_id: key,
            //                times_captured: this.pkmns[key].captured,
            //                 times_encountered: this.pkmns[key].encountered
            //            }
            //        }
            //    });
            //};
            //return (out);
            return null;
        }
    }

    public class Poke
    {
        public ushort captured { get; set; }
        public ushort encountered { get; set; }
    }
}