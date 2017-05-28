using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Machine;
using PoGoEmulator.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace PoGoEmulator
{
    internal class Program
    {
        public static PogoMachine machine;

        public static void Garbage()
        {
#if DEBUG
            Logger.Write("GarbageCollector is working", LogLevel.Debug);
#endif
            new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    GC.Collect();
                    Thread.Sleep((int)Global.Cfg.GarbageTime.TotalMilliseconds);
                }
            })).Start();
        }

        private static void Main(string[] args)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

                Logger.AddLogger(new ConsoleLogger(LogLevel.Info));

#if DEBUG
                Logger.Write("ON", LogLevel.Debug);
#endif
                Garbage();
                Assets.ValidateAssets();

                Global.GameMaster = new GameMaster();

                machine = new PogoMachine();
                string line = "";
                do
                {
                    line = Console.ReadLine();
                    switch (line)
                    {
                        case "help":
                            Logger.Write(" - help menu", LogLevel.Help);
                            break;
                    }
                } while (line != "exit");
            }
            catch (Exception e)
            {
                Logger.Write(e);
            }
            machine?.Stop();
            Console.ReadLine();
        }
    }
}