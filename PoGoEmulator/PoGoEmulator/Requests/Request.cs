using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using System;
using System.Linq;
using System.Threading;

namespace PoGoEmulator.Requests
{
    public static class RequestHandler
    {
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
            catch (ObjectDisposedException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
#endif
            }
            catch (OperationCanceledException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
#endif
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, Enums.LogLevel.Error);
            }
        }

        // "POGOProtos.Networking.Envelopes.RequestEnvelope"
        private static void RpcRequestParser(Connection connectedClient, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            if (!connectedClient.HttpContext.Body.Any())
                throw new Exception("request body is empty");

            RequestEnvelope rqs = connectedClient.HttpContext.Body.First().Proton<RequestEnvelope>();
            //Logger.Write(rqs.ToString(), LogLevel.Response);
        }
    }
}