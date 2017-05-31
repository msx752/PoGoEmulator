using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("custom")]
    public class CustomController : BaseRpcController
    {
        public CustomController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(this.GetType());
            RpcType = Enums.RpcRequestType.Custom;
        }

        [ActionName("rpc")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Rpc()
        {
            return base.OnRequest();
        }
    }
}