using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PoGoEmulator.Assets;

namespace PoGoEmulator
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls($"http://{GlobalSettings.Cfg.Ip}:{GlobalSettings.Cfg.Port}")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}