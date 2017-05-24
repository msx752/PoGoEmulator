using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using PoGoPrivate.Machine;
using PoGoPrivate.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoGoPrivate
{
    internal class Program
    {
        private static PogoMachine machine;

        private static void Main(string[] args)
        {
            try
            {
                Logger.AddLogger(new ConsoleLogger(LogLevel.Info));

#if DEBUG
                Logger.Write("ON", LogLevel.Debug);
#endif
                Garbage();
                Assets.ValidateAssets();

                Global.GameMaster = new GameMaster();

                Task run = Task.Factory.StartNew(() =>
                {
                    machine = new PogoMachine();
                    machine.Run();
                });
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
                Logger.Write(e.Message, LogLevel.Error);
            }
            machine.Stop();
        }

        public static void Garbage()
        {
            #region Start GC Collector

            Task.Run(() =>
            {
                while (true)
                {
                    GC.Collect();
                    Thread.Sleep((int)Global.Cfg.GarbageTime.TotalMilliseconds);
                }
            });

            #endregion Start GC Collector
        }
    }
}