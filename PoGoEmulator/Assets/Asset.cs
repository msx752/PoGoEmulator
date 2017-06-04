using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using POGOProtos.Networking.Responses;

namespace PoGoEmulator.Assets
{
    public class Asset
    {
        public static string[] Plaforms = new string[] { "android", "ios" };

        public static void ValidateAssets()
        {
            Console.WriteLine("Validating Assets..");

            if (!Directory.Exists(Path.Combine(GlobalSettings.Cfg.DUMP_ASSET_PATH)))
#if DEBUG

                throw new Exception("'Data' folder not found.download[https://mega.nz/#!aEBGmZ7b!EwSmPmyJxcO0PYUYzuk5Suy3s8j-V99yvz0oMTtEmnI] ,unzip and put to \\bin\\data folder");
#else
             throw new Exception("'Data' folder not found.download[https://mega.nz/#!aEBGmZ7b!EwSmPmyJxcO0PYUYzuk5Suy3s8j-V99yvz0oMTtEmnI] ,unzip and put to \\data folder");
#endif

            if (!File.Exists(Path.Combine(GlobalSettings.Cfg.DUMP_ASSET_PATH, "game_master")))
                throw new Exception("'game_master' not found");

            //#if DEBUG
            //            Logger.Write("Pokemons are loading from 'asset_digest'..", Enums.LogLevel.Debug);
            //#endif
            var max = GlobalSettings.Cfg.MAX_POKEMON_NATIONAL_ID;
            var limit = Plaforms.Length;

            for (int i = 0; i < limit; i++)
            {
                var platform = Plaforms[i];
                var path_platform = Path.Combine(GlobalSettings.Cfg.DUMP_ASSET_PATH, platform);
                var path_asset_digest = Path.Combine(path_platform, "asset_digest");
                if (!File.Exists(path_asset_digest))
                    throw new Exception("'asset_digest' not found");
                else
                {
                    using (var stream = File.OpenRead(path_asset_digest))
                    {
                        BinaryReader sr = new BinaryReader(stream);
                        byte[] buffer = sr.ReadBytes((int)sr.BaseStream.Length);
                        GlobalSettings.GameAssets[platform] =
                            new KeyValuePair<byte[], GetAssetDigestResponse>(buffer, buffer.ProtoSerializer<GetAssetDigestResponse>());
                    }
                }

                for (var j = 1; j <= max; j++)
                {
                    var modelFile = "pm" + (j >= 10 ? j >= 100 ? "0" : "00" : "000") + j;
                    var path = Path.Combine(path_platform, modelFile);
                    if (!File.Exists(path))
                        throw new Exception($"{path} not found");
                }
            }
            Console.WriteLine("Pokemons are successfully loaded...");
        }
    }
}