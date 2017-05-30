using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PoGoEmulatorApi.Models
{
    public class Configs
    {
        public string DUMP_ASSET_PATH { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "bin\\data";
        public TimeSpan GarbageTime { get; set; } = new TimeSpan(0, 0, 10);
        public int MAX_POKEMON_NATIONAL_ID { get; set; } = 151;
        public int MaxRequestContentLength { get; set; } = (1024 * 1024) * (1);//1MB

        //public TimeSpan RequestTimeout { get; set; } = new TimeSpan(0, 0, 15);

        public string SqlConnectionString { get; set; } = @"Server=.\sqlexpress;Database=pogodb;Trusted_Connection=True;";
    }
}