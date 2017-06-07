using System.Collections.Generic;

namespace PoGoEmulator.Models.ViewModel
{
    public class AdminModel
    {
        public int OnlineUserCount { get; set; }
        public Dictionary<string, string> SpawnLocations { get; set; }
    }
}