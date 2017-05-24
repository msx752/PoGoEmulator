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
            Logger.AddLogger(new ConsoleLogger(LogLevel.Info));
            Garbage();
            Assets.ValidateModels();
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
                    Thread.Sleep((int)Global.GarbageTime.TotalMilliseconds);
                }
            });

            #endregion Start GC Collector
        }
    }
}