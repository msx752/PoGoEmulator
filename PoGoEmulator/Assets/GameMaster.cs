using System;
using System.Collections.Generic;
using System.IO;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator.Assets
{
    public class GameMaster
    {
        public GameMaster()
        {
            //#if DEBUG

            Console.WriteLine("ItemTemplates are loading from 'game_master'..");
            //#endif
            Settings["PLAYER_LEVEL_SETTINGS"] = null;//move to top
            var path_game_master = Path.Combine(GlobalSettings.ServerCfg.DUMP_ASSET_PATH, "game_master");
            if (!File.Exists(path_game_master))
                throw new Exception($"{path_game_master} not found");

            using (var stream = File.OpenRead(path_game_master))
            {
                BinaryReader br = new BinaryReader(stream);
                Buffer = br.ReadBytes((int)br.BaseStream.Length);
                Decode = Buffer.ProtoSerializer<DownloadItemTemplatesResponse>();

                foreach (var item in Decode.ItemTemplates)
                    this.Settings[item.TemplateId] = item;
            }
            Console.WriteLine("ItemTemplates are successfully loaded\r\n");
        }

        public byte[] Buffer { get; set; }

        public DownloadItemTemplatesResponse Decode { get; set; }

        public Dictionary<string, DownloadItemTemplatesResponse.Types.ItemTemplate> Settings { get; set; }
                                    = new Dictionary<string, DownloadItemTemplatesResponse.Types.ItemTemplate>();
    }
}