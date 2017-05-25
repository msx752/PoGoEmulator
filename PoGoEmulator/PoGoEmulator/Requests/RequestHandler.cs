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
    /// <summary>
    /// server request handler 
    /// </summary>
    public static partial class RequestHandler
    {
        /// <summary>
        /// global authenticated user informations 
        /// </summary>
        public static ConcurrentDictionary<string, string> AuthedUserTokens { get; private set; }

        static RequestHandler()
        {
            //MICROSOFT EXPLANATION: https://msdn.microsoft.com/tr-tr/library/dd287191(v=vs.110).aspx
            int initialCapacity = 101;//is it auto increase ?
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;
            AuthedUserTokens = new ConcurrentDictionary<string, string>(concurrencyLevel, initialCapacity);
        }

        /// <summary>
        /// request router 
        /// </summary>
        /// <param name="connectedClient">
        /// </param>
        /// <param name="ct">
        /// </param>
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
                        throw new Exception($"Undefined request url: {url}");
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