using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Requests;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using PoGoEmulator.Database;
using PoGoEmulator.Database.Tables;
using Timer = System.Timers.Timer;

namespace PoGoEmulator.Models
{
    public sealed class Connection : IDisposable
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private MyHttpContext _httpContext;
        private TcpClient client;
        private bool IsDisposed = false;
        private Stopwatch stopwatch;
        private NetworkStream _stream;
        private Timer tmr;
        private PoGoDbContext _database;

        public Connection(TcpClient client)
        {
            _cts.Token.ThrowIfCancellationRequested();
            this.client = client;
            stopwatch = new Stopwatch();
            tmr = new Timer(150);

            tmr.Elapsed += Tmr_Elapsed;
            stopwatch.Start();
            Task.Run(() => tmr.Start(), _cts.Token);
            this._stream = this.client.GetStream();
            _httpContext = Stream.GetContext(_cts.Token);
            _database = new PoGoDbContext();
        }

        public PoGoDbContext Database { get { return _database; } }
        public MyHttpContext HttpContext { get { return _httpContext; } }
        public NetworkStream Stream { get { return _stream; } }

        public void Abort(RequestState state)
        {
            //You can write out an abort message to the client if you like. (Stream.Write()....)
            this.Dispose(state);
        }

        private void Dispose(RequestState state)
        {
            if (IsDisposed || _cts.IsCancellationRequested) return;
#if DEBUG
            Logger.Write($"session ended for {client.Client.RemoteEndPoint}, Reason: '{state}'", LogLevel.Debug);
#endif
            IsDisposed = true;

            if (state == RequestState.Completed)
                Database.SaveChanges();

            Database.Dispose();

            tmr.Enabled = false;
            tmr.Dispose();
            _cts.Cancel(); //force stop
            Thread.Sleep(10);
            tmr = null;

            stopwatch = null;
            _httpContext = null;

            client?.Close();
            ((IDisposable)client)?.Dispose();
            client = null;

            _stream?.Close();
            _stream?.Dispose();
            _stream = null;
        }

        public void Dispose()
        {
            Dispose(RequestState.Completed);
        }

        public void Execute()
        {
            try
            {
#if DEBUG
                Logger.Write($"{HttpContext.RequestUri} from {client.Client.RemoteEndPoint}", LogLevel.Response);
#endif
                RequestHandler.Parse(this, _cts.Token);
            }
            catch (ObjectDisposedException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
                this.Abort(RequestState.AbortedBySystem);
                return;
#endif
            }
            catch (OperationCanceledException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
                this.Abort(RequestState.AbortedBySystem);
                return;
#endif
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, LogLevel.Error);
                this.Abort(RequestState.AbortedBySystem);
                return;
            }
            this.Abort(RequestState.Completed);
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_cts.Token.IsCancellationRequested)
                return;

            if (this.client.Client.Poll(1, SelectMode.SelectRead) && this.client.Client.Available == 0)//detect the custom aborting
                this.Abort(RequestState.CanceledByUser);
            else if (stopwatch?.ElapsedMilliseconds > Global.Cfg.RequestTimeout.TotalMilliseconds)
                this.Abort(RequestState.Timeout);
        }
    }
}