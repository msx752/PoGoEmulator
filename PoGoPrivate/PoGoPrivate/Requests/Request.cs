using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;

namespace PoGoPrivate.Requests
{
    public static class Request
    {
        public static void Router(Connection connectedClient, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                string router = "";
                if (connectedClient.Headers.ContainsKey("POST"))
                    router = connectedClient.Headers["POST"];
                //else if (connectedClient.Headers.ContainsKey("GET")) //check it
                //    router = connectedClient.Headers["GET"];
                else
                    throw new Exception($"unkown request format: {connectedClient.Headers.FirstOrDefault().Key}");

                router = router.Replace(" HTTP/1.1", "");//remove http the version
                Uri url = new Uri("http://" + connectedClient.Headers["Host"] + router);
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
                        ProcessRpcRequest(connectedClient, ct);
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

        private static void ProcessRpcRequest(Connection connectedClient, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
        }
    }
}