using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoGoEmulator.Database;

namespace PoGoEmulator.Controllers
{
    public class CustomController : BaseRpcController
    {
        public CustomController(PoGoDbContext db, ILoggerFactory loggerf) : base(db, loggerf)
        {
            Log = loggerf.CreateLogger(this.GetType());
            RpcType = Enums.RpcRequestType.Custom;
        }

        [HttpPost]
        public override HttpResponseMessage Rpc()
        {
            return base.Rpc();
        }
    }
}