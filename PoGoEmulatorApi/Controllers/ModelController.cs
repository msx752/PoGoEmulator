using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("model")]
    public class ModelController : FunctionController4
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
            Log.Dbg("MODELCONTROLELR NOT DECLARED YET");
            return null;
            //return base.Rpc();
        }
    }
}