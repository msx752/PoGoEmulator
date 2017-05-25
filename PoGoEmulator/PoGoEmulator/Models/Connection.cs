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
        private TcpClient client;
        private bool _isDisposed;
        private TimeoutTick _ttick;

        public Connection(TcpClient client)
        {
            _cts.Token.ThrowIfCancellationRequested();
            this.client = client;
            _ttick = new TimeoutTick(_cts.Token, TimeoutChecker, true);
            Stream = this.client.GetStream();
            HttpContext = Stream.GetContext(_cts.Token);
            Database = new PoGoDbContext();
        }

        public PoGoDbContext Database { get; }
        public MyHttpContext HttpContext { get; private set; }
        public NetworkStream Stream { get; private set; }

        public void Abort(RequestState state)
        {
            //You can write out an abort message to the client if you like. (Stream.Write()....)
            Dispose(state);
        }

        private void Dispose(RequestState state)
        {
            if (_isDisposed || _cts.IsCancellationRequested) return;
#if DEBUG
            Logger.Write($"session ended for {client.Client.RemoteEndPoint}, Reason: '{state}'", LogLevel.Debug);
#endif
            _isDisposed = true;

            if (state == RequestState.Completed)
                Database.SaveChanges();

            Database.Dispose();
            _ttick.Stop();
            _ttick = null;
            _cts.Cancel(); //force stop
            HttpContext = null;

            client?.Close();
            ((IDisposable)client)?.Dispose();
            client = null;

            Stream?.Close();
            Stream?.Dispose();
            Stream = null;
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
                Abort(RequestState.AbortedBySystem);
                return;
#endif
            }
            catch (OperationCanceledException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
                Abort(RequestState.AbortedBySystem);
                return;
#endif
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, LogLevel.Error);
                Abort(RequestState.AbortedBySystem);
                return;
            }
            Abort(RequestState.Completed);
        }

        private void TimeoutChecker()
        {
            if (_cts.Token.IsCancellationRequested)
                return;

            if (client.Client.Poll(1, SelectMode.SelectRead) && client.Client.Available == 0)//detect the custom aborting
                Abort(RequestState.CanceledByUser);
            else if (_ttick.Stopwatch.ElapsedMilliseconds > Global.Cfg.RequestTimeout.TotalMilliseconds)
                Abort(RequestState.Timeout);
        }
    }
}