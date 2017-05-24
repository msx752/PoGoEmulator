using PoGoPrivate.Models;
using System;
using System.Net;

namespace PoGoPrivate
{
    public static class Global
    {
        public static Configs Cfg = new Configs()
        {
#if DEBUG
            RequestTimeout = new TimeSpan(0, 10, 0)
#endif
        };
    }
}