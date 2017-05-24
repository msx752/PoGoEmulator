using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using System;
using System.Linq;
using System.Threading;

namespace PoGoEmulator.Requests
{
    public static partial class RequestHandler
    {
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