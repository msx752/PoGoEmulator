using System;
using System.Net;

namespace PoGoPrivate.Models
{
    public class Configs
    {
        public int Port { get; set; } = 3000;
        public IPAddress Ip { get; set; } = IPAddress.Any;
        public int MaxRequestContentLength { get; set; } = (1024 * 1024) * (1); //1MB
        public TimeSpan RequestTimeout { get; set; } = new TimeSpan(0, 0, 15);
        public TimeSpan GarbageTime { get; set; } = new TimeSpan(0, 0, 10);
        public string DUMP_ASSET_PATH { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "data";

        public int MAX_POKEMON_NATIONAL_ID { get; set; } = 151;
    }
}