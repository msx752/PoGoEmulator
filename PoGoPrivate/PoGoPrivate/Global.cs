using PoGoPrivate.Models;
using System;
using System.Net;

namespace PoGoPrivate
{
    public static class Global
    {
        public static Configs cfg = new Configs();
        public static int port = 3000;
        public static IPAddress ip = IPAddress.Any;
        public static int maxRequestContentLength = (1024 * 1024) * (1); //1MB
#if DEBUG

        public static TimeSpan requestTimeout = new TimeSpan(0, 10, 0);
#else
        public static TimeSpan requestTimeout = new TimeSpan(0, 0, 15);
#endif
        public static TimeSpan garbageTime = new TimeSpan(0, 0, 10);
    }
}