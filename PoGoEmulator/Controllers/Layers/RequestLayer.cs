using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Server;
using PoGoEmulator.Database;
using PoGoEmulator.Enums;
using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator.Controllers.Layers
{
    public class RequestLayer : Controller
    {
        protected RpcRequestType RpcType { get; set; } = RpcRequestType.None;

        private RequestEnvelope _requestProto;

        protected RequestEnvelope ProtoRequest
        {
            get
            {
                LoadProtoContent();
                return _requestProto;
            }
            set { _requestProto = value; }
        }

        public RequestLayer(PoGoDbContext db, ILoggerFactory loggerf)
        {
            _requestProto = null;
            this.Database = db;
            ProtoResponse = new ResponseEnvelope();
        }

        protected ResponseEnvelope ProtoResponse { get; set; }
        public PoGoDbContext Database { get; set; }
        protected ILogger Log { get; set; } = null;

        private void LoadProtoContent()
        {
            if (_requestProto != null) return;

            if (RpcType == RpcRequestType.None) return;

            _requestProto = new RequestEnvelope();

            using (var bodyReader = new BinaryReader(Request.Body))
            {
                Byte[] buf = new byte[GlobalSettings.Cfg.MaxRequestContentLength];
                int lent = bodyReader.Read(buf, 0, buf.Length);
                Array.Resize(ref buf, lent);
                CodedInputStream cis = new CodedInputStream(buf);
                _requestProto.MergeFrom(cis);
            }
        }

        protected HttpResponseMessage ThrowException(Exception e)
        {
            return this.ResponseToClient(HttpStatusCode.BadRequest, e);
        }

        protected HttpResponseMessage ResponseToClient(HttpStatusCode code, Exception e = null)
        {
            HttpResponseMessage res = new HttpResponseMessage(code);
            res.Headers.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
            res.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            res.Headers.TryAddWithoutValidation("PoGoEmulator", ".NET 4.6.2 MVC API");
            res.Headers.TryAddWithoutValidation("Date", $"{string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT");
            if (code == HttpStatusCode.OK)
            {
                Database.SaveChanges();
                Log.LogInformation($"succesfully responding[{Request.Host.Host}]:");
            }
            else
            {
                ProtoResponse = new ResponseEnvelope()
                {
                    RequestId = ProtoRequest.RequestId,
                    Error = e.Message,
                    StatusCode = 1,
                };
                Log.LogError($"{DateTime.Now.ToString()} responding error[{Request.Host.Host}]: {e.Message}");
            }

            res.Content = new ByteArrayContent(ProtoResponse.ToByteArray());
            return res;
        }
    }
}