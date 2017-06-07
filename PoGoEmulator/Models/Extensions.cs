using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoGoEmulator.Models
{
    public static class Extensions
    {
        public static bool ValidUsername(string name)
        {
            var rx_username = "/[^a-z\\d]/i";
            var r = Regex.Match(name, rx_username);
            return !r.Success;
        }

        public static bool ValidEmail(string email)
        {
            var rx_username = "/\\S+@\\S+\\.\\S+/";
            var r = Regex.Match(email, rx_username);
            return r.Success;
        }

        public static string IdToPkmnBundleName(int index)
        {
            return "pm" + (index >= 10 ? index >= 100 ? "0" : "00" : "000") + index;
        }

        public static double RandomRequestId()
        {
            Random r = new Random();
            return 1e18 - Math.Floor(r.NextDouble() * 1e18);
        }
    }
}