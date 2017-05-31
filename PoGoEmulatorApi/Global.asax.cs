using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using PoGoEmulatorApi.Assets;
using PoGoEmulatorApi.Controllers;
using PoGoEmulatorApi.Models;

namespace PoGoEmulatorApi
{
    //netsh http add urlacl url=http://*:3000/ user=Everyone
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ConcurrentDictionary<string, CacheUserData> AuthenticatedUsers { get; set; } =
              new ConcurrentDictionary<string, CacheUserData>();//it must be re-configure in the future because user must be logout in somewhere (do i need store authTicket too ?)

        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            log4net.Config.XmlConfigurator.Configure();
            Asset.ValidateAssets();
            GlobalSettings.GameMaster = new GameMaster();
        }
    }
}