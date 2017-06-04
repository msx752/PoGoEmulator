using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoGoEmulator.Database;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator.Controllers.Layers
{
    public class ResponseLayer : RequestLayer
    {
        public ResponseLayer(PoGoDbContext db, ILoggerFactory loggerf)
            : base(db, loggerf)
        {
        }

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