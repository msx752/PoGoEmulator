using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web;
using Google.Protobuf;
using Google.Protobuf.Collections;
using POGOProtos.Networking.Envelopes;

// ReSharper disable once CheckNamespace
namespace PoGoEmulatorApi.Controllers
{
    public class ResponseController2 : RequestController1
    {
        public ResponseController2(PoGoDbContext db) : base(db)
        {
        }

        protected HttpResponse Response { get { return HttpContext.Response; } }

        [System.Web.Http.NonAction]
        protected HttpResponseMessage EnvelopResponse(RepeatedField<ByteString> returns = null)
        {
            this.ProtoResponse.RequestId = this.ProtoRequest.RequestId;
            if (returns != null)
            {
                //this.Log.Dbg($"ReturnsCount:{returns.Count}");
                this.ProtoResponse.Returns.AddRange(returns);
            }
            else
            {
                //this.Log.Dbg($"ReturnsCount:null");
            }

            if (this.ProtoRequest.AuthTicket != null)
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
            return base.ResponseToClient(HttpStatusCode.OK);
        }
    }
}