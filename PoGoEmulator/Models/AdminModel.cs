using System;
using System.Collections.Generic;
using System.Text;

namespace PoGoEmulator.Models
{
    public class AdminModel
    {
        public int OnlineUserCount { get; set; }
        public Dictionary<string, string> SpawnLocations { get; set; }
    }
}