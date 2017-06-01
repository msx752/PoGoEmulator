#region using directives

using System;
using System.Windows.Forms;
using PoGoEmulator.Forms;
using PoGoEmulator.Win32;

#endregion

namespace PoGoEmulator
{
    internal class Program
    {
        [STAThread]

        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            ConsoleHelper.AllocConsole();
            ConsoleHelper.HideConsoleWindow();
            Application.Run(new MainForm(args));
        }
    }
}