using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POGOProtos.Enums;
using POGOProtos.Networking.Responses;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models.GameMasters
{
    public static class GameMasterExtensions
    {
        public static PlayerLevelSettings GetPlayerSettings(this GameMaster g)
        {
            var item = GlobalSettings.GameMaster.Settings["PLAYER_LEVEL_SETTINGS"];
            return item.PlayerLevel;
        }

        public static PokemonSettings GetPokemonTmplByDex(this GameMaster g, byte dex)
        {
            var id = Extensions.IdToPkmnBundleName(dex);
            PokemonId name = (PokemonId)dex;
            string tmplId = $"V{id}_POKEMON_{name}";

            var items = g.Decode.ItemTemplates;
            foreach (var item in items)
            {
                if (item.PokemonSettings.IsNotNull() && item.TemplateId == tmplId)
                {
                    return item.PokemonSettings;
                }
            }
            return null;
        }
    }
}