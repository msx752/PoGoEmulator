using PoGoEmulator.Enums;
using PoGoEmulator.Forms;
using PoGoEmulator.Logging;
using PoGoEmulator.Machine;
using PoGoEmulator.Models;
using PoGoEmulator.Win32;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoGoEmulator
{
    internal class Program
    {
        public static PogoMachine machine;

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

        private static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //for app winexe
            //ConsoleHelper.AllocConsole();
            //Application.Run(new MainForm(args));

            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

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
                            /*case "gui":
                                StartGui();
                                break;*/
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