using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoGoEmulator.Controllers.Layers;
using PoGoEmulator.Database;

namespace PoGoEmulator.Controllers
{
    public class BaseRpcController : FunctionLayer
    {
        public BaseRpcController(PoGoDbContext db, ILoggerFactory loggerf) : base(db, loggerf)
        {
        }

        [HttpPost]
        public virtual HttpResponseMessage Rpc()
        {
            return base.OnRequest();
        }
    }
}