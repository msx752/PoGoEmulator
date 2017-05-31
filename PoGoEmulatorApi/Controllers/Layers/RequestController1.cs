using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Google.Protobuf;
using log4net;
using PoGoEmulatorApi.Enums;
using POGOProtos.Networking.Envelopes;

// ReSharper disable once CheckNamespace
namespace PoGoEmulatorApi.Controllers
{
    public class RequestController1 : ApiController
    {
        public RequestController1(PoGoDbContext db)
        {
            this.Database = db;
            ProtoResponse = new ResponseEnvelope();
        }

        public HttpRequestMessage HttpRequestMessage { get { return HttpContext.Items["MS_HttpRequestMessage"] as HttpRequestMessage; } }

        protected RpcRequestType RpcType { get; set; } = RpcRequestType.None;
        private RequestEnvelope _requestProto;
        protected RequestEnvelope ProtoRequest { get { LoadProtoContent(); return _requestProto; } set { _requestProto = value; } }

        protected new HttpRequest Request { get { return HttpContext.Request; } }
        protected HttpContext HttpContext { get { return HttpContext.Current; } }

        private void LoadProtoContent()
        {
            if (_requestProto != null) return;
            if (RpcType == RpcRequestType.None) return;

            _requestProto = new RequestEnvelope();
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);

            Byte[] buf = new byte[req.Length];
            req.Read(buf, 0, buf.Length);
            Google.Protobuf.CodedInputStream cis = new CodedInputStream(buf);
            _requestProto.MergeFrom(cis);
        }

        [System.Web.Http.NonAction]
        protected HttpResponseMessage ThrowException(Exception e)
        {
            return this.ResponseToClient(HttpStatusCode.BadRequest, e);
        }

        protected ResponseEnvelope ProtoResponse { get; set; }
        public PoGoDbContext Database { get; set; }
        protected ILog Log { get; set; } = null;

        [System.Web.Http.NonAction]
        protected HttpResponseMessage ResponseToClient(HttpStatusCode code, Exception e = null)
        {
            HttpResponseMessage res = new HttpResponseMessage(code);
            res.Headers.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
            res.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            res.Headers.TryAddWithoutValidation("PoGoEmulator", ".NET 4.6.2 MVC API");
            res.Headers.TryAddWithoutValidation("Date", $"{string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT");
            if (code == HttpStatusCode.OK)
            {
                //res.Content = new ByteArrayContent(ProtoResponse.ToByteArray());
                Database.SaveChanges();
                Log.Dbg($"succesfully responding");
            }
            else
            {
                ProtoResponse = new ResponseEnvelope()
                {
                    RequestId = ProtoRequest.RequestId,
                    Error = e.Message,
                    StatusCode = 1,
                };
                Log.Error("unsuccesfully responding error:", e);
            }
            res.Content = new ByteArrayContent(ProtoResponse.ToByteArray());
            return res;
        }
    }
}