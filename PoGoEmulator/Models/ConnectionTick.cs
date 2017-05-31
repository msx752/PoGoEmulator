using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using PoGoEmulator.Enums;
using Timer = System.Timers.Timer;

namespace PoGoEmulator.Models
{
    public class ConnectionTick
    {
        private Connection2 _conn;
        private CancellationToken _ct;
        private System.Timers.Timer _tmr;

        public ConnectionTick(CancellationToken ct)
        {
            _ct = ct;
            Stopwatch = new Stopwatch();
            _tmr = new Timer(1000);
            _tmr.Elapsed += Tmr_Elapsed;
        }

        /// <summary>
        /// </summary>
        /// <param name="ct">
        /// action cancelation token 
        /// </param>
        /// <param name="elapsedMethod">
        /// method which triggered with every tick 
        /// </param>
        /// <param name="conn">
        /// </param>
        /// <param name="startAfterCreate">
        /// auto starts the function 
        /// </param>
        public ConnectionTick(CancellationToken ct, Connection2 conn, bool startAfterCreate) : this(ct)
        {
            _conn = conn;
            if (startAfterCreate)
                this.Start();
        }

        public Stopwatch Stopwatch { get; set; }

        public void Start()
        {
            if (_tmr.Enabled) throw new Exception("timeouter already activated");
            Stopwatch.Start();
            Task.Run(() => _tmr.Start(), _ct);
        }

        public void Stop()
        {
            _tmr?.Stop();
            _tmr?.Dispose();
            Stopwatch?.Stop();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_ct.IsCancellationRequested)
            {
                Stop();
                return;
            }

            try
            {
                if (_conn.TCP.Client.Poll(1, SelectMode.SelectRead) && _conn.TCP.Client.Available == 0)//detect the custom aborting
                    _conn.Abort(RequestState.CanceledByUser, new Exception("canceled"));
                else if (Stopwatch.ElapsedMilliseconds > Global.Cfg.RequestTimeout.TotalMilliseconds)
                    _conn.Abort(RequestState.Timeout, new Exception("connectionTimeout"));
            }
            catch
            {
                // ignored
            }
        }
    }
}