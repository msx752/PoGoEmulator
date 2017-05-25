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
    /// <summary>
    /// request timeout checker 
    /// </summary>
    public class TimeoutTick
    {
        private Timer _tmr;

        public Stopwatch Stopwatch { get; set; }
        private CancellationToken _ct;
        private Action _elapsedMethod;

        public TimeoutTick(CancellationToken ct)
        {
            _ct = ct;
            Stopwatch = new Stopwatch();
            _tmr = new Timer(150);
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
        /// <param name="startAfterCreate">
        /// auto starts the function 
        /// </param>
        public TimeoutTick(CancellationToken ct, Action elapsedMethod, bool startAfterCreate) : this(ct)
        {
            _elapsedMethod = elapsedMethod;
            if (startAfterCreate)
                this.Start();
        }

        public void Start()
        {
            if (_tmr.Enabled) throw new Exception("timeouter already activated");
            Stopwatch.Start();
            Task.Run(() => _tmr.Start(), _ct);
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            _elapsedMethod();
        }

        public void Stop()
        {
            _tmr.Stop();
            _elapsedMethod = null;

            Stopwatch = null;
        }
    }
}