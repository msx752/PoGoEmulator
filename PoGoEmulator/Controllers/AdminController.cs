using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoGoEmulator.Models;

namespace PoGoEmulator.Controllers
{
    // [Authorize]//must be
    public class AdminController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new AdminModel()
            {
                OnlineUserCount = GlobalSettings.AuthenticatedUsers.Count,
                SpawnLocations = new Dictionary<string, string>()//fake
                  {
                      {"41.0141674,28.9792187","Pikachu" },
                      {"41.0455243,29.0224406","Raichu" }
                  }
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}