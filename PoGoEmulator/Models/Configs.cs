using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PoGoEmulator.Models
{
    public class ServerSettings
    {
#if DEBUG
        public string DUMP_ASSET_PATH { get; set; } = Directory.GetCurrentDirectory() + "\\bin\\Data";
#else
        public string DUMP_ASSET_PATH { get; set; } = Directory.GetCurrentDirectory() + "\\Data";
#endif
        public TimeSpan GarbageTime { get; set; } = new TimeSpan(0, 0, 10);
        public String Ip { get; set; } = "192.168.2.248";
        public int MAX_POKEMON_NATIONAL_ID { get; set; } = 151;
        public int MaxRequestContentLength { get; set; } = (1024 * 1024) * (1);//1MB
        public int Port { get; set; } = 3000;

        public TimeSpan RequestTimeout { get; set; } = new TimeSpan(0, 0, 15);

        public string SqlConnectionString { get; set; } = @"Server=.\sqlexpress;Database=pogodb;Trusted_Connection=True;";
    }
}