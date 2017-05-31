using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpMachine;
using PoGoEmulator.Database;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using POGOProtos.Networking.Envelopes;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models
{
    public class BaseConnection
    {
        public TcpClient TCP { get; }

        public CancellationTokenSource _cts { get; }
        public HttpNetworkStream Stream { get; }
        public PoGoDbContext Database { get; }

        public HttpStreamContext StreamContext { get; }
        public ResponseEnvelope ProtoResponse { get; set; }

        protected BaseConnection(TcpClient client, CancellationTokenSource cancelToken)
        {
            ProtoResponse = new ResponseEnvelope();
            Database = new PoGoDbContext();
            TCP = client;
            _cts = cancelToken;
            Stream = new HttpNetworkStream(TCP.GetStream());
            StreamContext = GetStreamContext();
        }

        private HttpStreamContext GetStreamContext()
        {
            try
            {
                var handler = new HttpStreamContext();
                var httpParser = new HttpParser(handler);
                var buffer = Stream.ReadBuffer(Global.Cfg.MaxRequestContentLength);
                var d = httpParser.Execute(new ArraySegment<byte>(buffer, 0, buffer.Length));
                if (buffer.Length != d)
                    throw new Exception("data not matching");

                return handler;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Abort(RequestState state, Exception e = null)
        {
            if (state == RequestState.Completed && e != null)
                throw new Exception("impossible request", e);

            Dispose(state, e);
        }

        private bool _isDisposed;

        private void Dispose(RequestState state, Exception e = null)
        {
            if (_isDisposed || _cts.IsCancellationRequested) return;
#if DEBUG
            Logger.Write($"session ended for {TCP.Client.RemoteEndPoint}, Reason: '{state}'", LogLevel.Debug);
#endif
            _isDisposed = true;

            if (state == RequestState.Completed)//response to user if request successful
            {
                Database.SaveChanges();
            }
            else
            {
                ProtoResponse = new ResponseEnvelope()
                {
                    RequestId = StreamContext.ProtoRequest.RequestId,
                    Error = e.Message,
                    StatusCode = 1,
                };
            }
            Stream.WriteProtoResponse(ProtoResponse);
            Stream.Close();
            TCP?.Close();
            ((IDisposable)TCP)?.Dispose();
            Database?.Dispose();
            _cts.Cancel(); //force stop
        }
    }
}