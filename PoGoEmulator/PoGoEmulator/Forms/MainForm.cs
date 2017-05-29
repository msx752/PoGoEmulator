#region using directives

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using POGOProtos.Data;
using POGOProtos.Inventory.Item;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using PoGoEmulator.CommandLineUtility;
using PoGoEmulator.Helpers;
using PoGoEmulator.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion


namespace PoGoEmulator.Forms
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public static MainForm Instance;
        public static SynchronizationContext SynchronizationContext;
        private string[] args;

        public MainForm(string[] _args)
        {
            InitializeComponent();
            SynchronizationContext = SynchronizationContext.Current;
            Instance = this;
            args = _args;
        }
        private static DateTime LastClearLog = DateTime.Now;

        public static void ColoredConsoleWrite(Color color, string text)
        {
            if (text.Length <= 0)
                return;

            if (Instance.InvokeRequired)
            {
                Instance.Invoke(new Action<Color, string>(ColoredConsoleWrite), color, text);
                return;
            }

            if (LastClearLog.AddMinutes(20) < DateTime.Now)
            {
                Instance.logTextBox.Text = string.Empty;
                LastClearLog = DateTime.Now;
            }

            Instance.logTextBox.SelectionColor = color;
            Instance.logTextBox.AppendText(text + $"\r\n");
            Instance.logTextBox.ScrollToCaret();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConsoleHelper.HideConsoleWindow();
        }
    }
}
