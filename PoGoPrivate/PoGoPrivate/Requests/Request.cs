using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using POGOProtos.Networking.Envelopes;

namespace PoGoPrivate.Requests
{
    public static class Request
    {
        public static void Router(Connection connectedClient, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                string router = connectedClient.HttpContext.requestUri;

                Uri url = new Uri("http://host" + router);
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
                        RpcRequest(connectedClient, ct);
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

        private static void RpcRequest(Connection connectedClient, CancellationToken ct)
        {
            // "POGOProtos.Networking.Envelopes.RequestEnvelope"
            ct.ThrowIfCancellationRequested();
            if (!connectedClient.HttpContext.body.Any())
            {
                Logger.Write("request body is empty", LogLevel.Error);
                return;
            }
            RequestEnvelope rqs = connectedClient.Proton<RequestEnvelope>();
            Logger.Write(rqs.ToString(), LogLevel.Response);
        }
    }
}