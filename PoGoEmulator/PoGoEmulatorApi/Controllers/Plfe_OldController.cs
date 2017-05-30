using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Google.Protobuf;
using PoGoEmulatorApi.Database;
using PoGoEmulatorApi.Models;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("plfe_old")]
    public class PlfeOLDController : BaseRpcController
    {
        public PlfeOLDController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            RpcType = Enums.RpcRequestType.Plfe;
            UpdatePlayerLocation();
            UpdateCachedUser();
        }

        [System.Web.Http.HttpPost]
        public override HttpResponseMessage Rpc()
        {
            if (!this.IsAuthenticated)
            {
                ProtoResponse.StatusCode = 53;
                ProtoResponse.RequestId = ProtoRequest.RequestId;
                ProtoResponse.ApiUrl = "pgorelease.nianticlabs.com/custom";
                ProtoResponse.AuthTicket = new AuthTicket()
                {
                    Start = ByteString.Empty,
                    ExpireTimestampMs = 1496119787409,
                    End = ByteString.Empty,
                };
            }

            return base.Rpc();
        }
    }
}