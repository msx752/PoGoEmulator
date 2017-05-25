using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace PoGoEmulator.Requests
{
    public static partial class RequestHandler
    {
        public static ConcurrentDictionary<string, string> UserTokens { get; private set; }

        static RequestHandler()
        {
            int initialCapacity = 101;
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;
            UserTokens = new ConcurrentDictionary<string, string>(concurrencyLevel, initialCapacity);
        }

        public static void Parse(Connection connectedClient, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                var router = connectedClient.HttpContext.RequestUri;

                var url = new Uri("http://host" + router);
                switch (url.Segments[1])
                {
                    case "plfe/":
                    case "custom/":
                        //for (int f = 0; f < 100000; f++)
                        //{
                        //    for (int i = 0; i < 100000; i++)
                        //    {
                        //        ct.ThrowIfCancellationRequested();
                        //        string s1 = i.ToString();
                        //    }
                        //}
                        RpcRequestParser(connectedClient, ct);
                        break;

                    case "model/":

                        break;

                    default:
                        throw new Exception($"Unknown request url: {url}");
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Write(e);
                connectedClient.Abort(RequestState.AbortedBySystem);
            }
        }
    }
}