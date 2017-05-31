using POGOProtos.Networking.Responses;
using System;
using System.Collections.Generic;
using System.IO;

namespace PoGoEmulatorApi.Assets
{
    public class GameMaster
    {
        public GameMaster()
        {
            //#if DEBUG

            //            Logger.Write("ItemTemplates are loading from 'game_master'..", LogLevel.Debug);
            //#endif
            Settings["PLAYER_LEVEL_SETTINGS"] = null;//move to top
            var path_game_master = Path.Combine(GlobalSettings.Cfg.DUMP_ASSET_PATH, "game_master");
            if (!File.Exists(path_game_master))
                throw new Exception($"{path_game_master} not found");

            BinaryReader br = new BinaryReader(new StreamReader(path_game_master).BaseStream);
            Buffer = br.ReadBytes((int)br.BaseStream.Length);
            Decode = Buffer.ProtoSerializer<DownloadItemTemplatesResponse>();

            foreach (var item in Decode.ItemTemplates)
                this.Settings[item.TemplateId] = item;

            //Logger.Write("ItemTemplates are successfully loaded");
        }

        public byte[] Buffer { get; set; }

        public DownloadItemTemplatesResponse Decode { get; set; }

        public Dictionary<string, DownloadItemTemplatesResponse.Types.ItemTemplate> Settings { get; set; }
                                    = new Dictionary<string, DownloadItemTemplatesResponse.Types.ItemTemplate>();
    }
}