using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("model")]
    public class ModelController : BaseRpcController
    {
        public ModelController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(this.GetType());
            RpcType = Enums.RpcRequestType.Model;
        }

        [ActionName("rpc")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Rpc()
        {
            return base.ThrowException(new System.Exception("MODELCONTROLELR NOT DECLARED YET"));
        }
    }
}