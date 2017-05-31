using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using HttpMachine;
using PoGoEmulator.Database;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using POGOProtos.Networking.Envelopes;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace

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

        protected HttpStreamResult EnvelopResponse(RepeatedField<ByteString> returns = null)
        {
            this.ProtoResponse.RequestId = this.StreamContext.ProtoRequest.RequestId;
            if (returns != null)
            {
                //this.Log.Dbg($"ReturnsCount:{returns.Count}");
                this.ProtoResponse.Returns.AddRange(returns);
            }
            else
            {
                //this.Log.Dbg($"ReturnsCount:null");
            }

            if (this.StreamContext.ProtoRequest.AuthTicket != null)
            {
                this.ProtoResponse.AuthTicket = new AuthTicket() { };
            }
            bool AlreadyExists = false;
            for (int i = 0; i < this.ProtoResponse.Unknown6.Count; i++)
            {
                if (this.ProtoResponse.Unknown6[i].ResponseType == 6)
                {
                    AlreadyExists = true;
                    break;
                }
            }
            if (AlreadyExists == false)
            {
                this.ProtoResponse.Unknown6.Add(new Unknown6Response()
                {
                    ResponseType = 6,
                    Unknown2 = new Unknown6Response.Types.Unknown2()
                    {
                        Unknown1 = 1
                    }
                });
            }
            this.ProtoResponse.StatusCode = 1;
            return Abort(RequestState.Completed);
        }

        public virtual HttpStreamResult Abort(RequestState state, Exception e = null)
        {
            if (state == RequestState.Completed && e != null)
                throw new Exception("impossible request", e);

            return Dispose(state, e);
        }

        private bool _isDisposed;

        private HttpStreamResult Dispose(RequestState state, Exception e = null)
        {
            if (_isDisposed || _cts.IsCancellationRequested) return null;
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
            var sresult = Stream.WriteProtoResponse(ProtoResponse);
            Stream?.Close();
            TCP?.Close();
            ((IDisposable)TCP)?.Dispose();
            Database?.Dispose();
            _cts.Cancel(); //force stop
            return sresult;
        }
    }
}