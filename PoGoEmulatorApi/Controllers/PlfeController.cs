using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("plfe")]
    public class PlfeController : AuthorizedController
    {
        public PlfeController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            RpcType = Enums.RpcRequestType.Plfe;
        }

        [System.Web.Http.HttpPost]
        public override HttpResponseMessage Rpc()
        {
            return OnRequest();
        }
    }
}