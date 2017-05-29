using System;
using System.Net;
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
            Client = client;
            Tmrtick = new TimeoutTick(_cts.Token, this, true);
            Stream = new HttpNetworkStream(Client.GetStream());
            Database = new PoGoDbContext();
        }

        public TcpClient Client { get; private set; }
        public PoGoDbContext Database { get; private set; }
        public MyHttpContext HttpContext { get; private set; }
        public HttpNetworkStream Stream { get; private set; }
        public TimeoutTick Tmrtick { get; private set; }

        public void Abort(RequestState state, Exception e = null)
        {
            if (state == RequestState.Completed && e != null)
                throw new Exception("impossible request", e);

            Dispose(state, e);
        }

        public void Answer()
        {
            try
            {
                HttpContext = Stream.GetContext(true);
                if (HttpContext.Request == null)
                    throw new Exception("'HttpContext.Request' is EMPTY");

                RequestHandler.Parse(this, _cts.Token);
            }
            catch (Exception e)
            {
                Logger.Write(e);
                Abort(RequestState.AbortedBySystem, e);
                return;
            }
            Abort(RequestState.Completed);
        }

        public void Dispose()
        {
            Abort(RequestState.Completed);
        }

        private void Dispose(RequestState state, Exception e = null)
        {
            if (_isDisposed || _cts.IsCancellationRequested) return;
#if DEBUG
            Logger.Write($"session ended for {Client.Client.RemoteEndPoint}, Reason: '{state}'", LogLevel.Debug);
#endif
            _isDisposed = true;

            if (state == RequestState.Completed)//response to user if request successful
            {
                Database.SaveChanges();
                Stream.WriteProtoResponse(HttpContext.Response);
            }
            else
            {
                Stream.WriteProtoResponse(HttpStatusCode.BadRequest, $"session ended Reason: '{state}', Message:'{e?.Message}'");
            }

            Stream.Close();
            Client?.Close();
            ((IDisposable)Client)?.Dispose();
            Database?.Dispose();
            Tmrtick?.Stop();
            Tmrtick = null;
            HttpContext = null;
            _cts.Cancel(); //force stop
        }
    }
}