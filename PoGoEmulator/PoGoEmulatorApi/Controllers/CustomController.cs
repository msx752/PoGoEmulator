using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Responses;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("custom")]
    public class CustomController : AuthorizedController
    {
        public CustomController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            RpcType = Enums.RpcRequestType.Custom;
            UpdateAuthorization();
        }

        [System.Web.Http.HttpPost]
        public override HttpResponseMessage Rpc()
        {
            return OnRequest();
        }
    }
}