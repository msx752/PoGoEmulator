﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using PoGoEmulatorApi.Database;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("model")]
    public class ModelController : BaseRpcController
    {
        public ModelController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            RpcType = Enums.RpcRequestType.Model;
        }

        [System.Web.Http.HttpPost]
        public override HttpResponseMessage Rpc()
        {
            return base.Rpc();
        }
    }
}