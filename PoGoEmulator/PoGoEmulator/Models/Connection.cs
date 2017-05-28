using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Google.Protobuf;
using PoGoEmulator.Database;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Requests;
using POGOProtos.Networking.Envelopes;

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
            Tmrtick = new TimeoutTick(_cts.Token, TimeoutChecker, true);
            Stream = Client.GetStream();
            Database = new PoGoDbContext();
            HttpContext = Stream.GetContext(_cts.Token, true);
        }

        public TcpClient Client { get; private set; }
        public PoGoDbContext Database { get; private set; }
        public MyHttpContext HttpContext { get; private set; }
        public NetworkStream Stream { get; private set; }
        public TimeoutTick Tmrtick { get; private set; }

        public void Abort(RequestState state, Exception e = null)
        {
            if (state == RequestState.Completed && e != null)
                throw new Exception("impossible request", e);
            //You can write out an abort message to the client if you like. (Stream.Write()....)
            Dispose(state, e);
        }

        public void Answer()
        {
            try
            {
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

            Database?.Dispose();
            Tmrtick?.Stop();
            Tmrtick = null;
            _cts.Cancel(); //force stop
            HttpContext = null;

            Client?.Close();

            ((IDisposable)Client)?.Dispose();

            Client = null;

            Stream?.Close();
            Stream?.Dispose();
            Stream = null;
        }

        private void TimeoutChecker()
        {
            if (_cts.Token.IsCancellationRequested)
                return;

            if (Client.Client.Poll(1, SelectMode.SelectRead) && Client.Client.Available == 0)//detect the custom aborting
                Abort(RequestState.CanceledByUser, new Exception("canceled"));
            else if (Tmrtick.Stopwatch.ElapsedMilliseconds > Global.Cfg.RequestTimeout.TotalMilliseconds)
                Abort(RequestState.Timeout, new Exception("connectionTimeout"));
        }
    }
}