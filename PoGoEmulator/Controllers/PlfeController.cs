using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoGoEmulator.Database;

namespace PoGoEmulator.Controllers
{
    public class PlfeController : BaseRpcController
    {
        public PlfeController(PoGoDbContext db, ILoggerFactory loggerf) : base(db, loggerf)
        {
            Log = loggerf.CreateLogger(this.GetType());
            RpcType = Enums.RpcRequestType.Plfe;
        }

        [HttpPost]
        public override HttpResponseMessage Rpc()
        {
            return base.Rpc();
        }
    }
}