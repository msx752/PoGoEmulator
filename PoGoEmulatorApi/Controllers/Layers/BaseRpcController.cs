using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ReSharper disable once CheckNamespace
namespace PoGoEmulatorApi.Controllers
{
    public class BaseRpcController : FunctionController4
    {
        public BaseRpcController(PoGoDbContext db) : base(db)
        {
        }
    }
}