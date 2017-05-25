using System;
using System.Net.Sockets;
using System.Threading;
using PoGoEmulator.Database;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Requests;

namespace PoGoEmulator.Models
{
    public sealed class Connection : IDisposable
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isDisposed;

        public Connection(TcpClient client)
        {
            _cts.Token.ThrowIfCancellationRequested();
            this.Client = client;
            Tmrtick = new TimeoutTick(_cts.Token, TimeoutChecker, true);
            Stream = this.Client.GetStream();
            HttpContext = Stream.GetContext(_cts.Token);
            Database = new PoGoDbContext();
        }

        public TcpClient Client { get; private set; }
        public PoGoDbContext Database { get; private set; }
        public MyHttpContext HttpContext { get; private set; }
        public NetworkStream Stream { get; private set; }
        public TimeoutTick Tmrtick { get; private set; }

        public void Abort(RequestState state)
        {
            //You can write out an abort message to the client if you like. (Stream.Write()....)
            Dispose(state);
        }

        public void Dispose()
        {
            Abort(RequestState.Completed);
        }

        public void Execute()
        {
            try
            {
#if DEBUG
                Logger.Write($"{HttpContext.RequestUri} from {Client.Client.RemoteEndPoint}", LogLevel.Response);
#endif
                RequestHandler.Parse(this, _cts.Token);
            }
            catch (Exception e)
            {
                Logger.Write(e);
                Abort(RequestState.AbortedBySystem);
                return;
            }
            Abort(RequestState.Completed);
        }

        private void Dispose(RequestState state)
        {
            if (_isDisposed || _cts.IsCancellationRequested) return;
#if DEBUG
            Logger.Write($"session ended for {Client.Client.RemoteEndPoint}, Reason: '{state}'", LogLevel.Debug);
#endif
            _isDisposed = true;

            if (state == RequestState.Completed)
                Database.SaveChanges();

            Database.Dispose();
            Tmrtick.Stop();
            Tmrtick = null;
            _cts.Cancel(); //force stop
            HttpContext = null;

            Client.Close();
            ((IDisposable)Client).Dispose();
            Client = null;

            Stream.Close();
            Stream.Dispose();
            Stream = null;
        }

        private void TimeoutChecker()
        {
            if (_cts.Token.IsCancellationRequested)
                return;

            if (Client.Client.Poll(1, SelectMode.SelectRead) && Client.Client.Available == 0)//detect the custom aborting
                Abort(RequestState.CanceledByUser);
            else if (Tmrtick.Stopwatch.ElapsedMilliseconds > Global.Cfg.RequestTimeout.TotalMilliseconds)
                Abort(RequestState.Timeout);
        }
    }
}