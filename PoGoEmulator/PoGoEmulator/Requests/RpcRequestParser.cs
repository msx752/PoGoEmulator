using PoGoEmulator.Models;
using System;
using System.Threading;
using PoGoEmulator.Responses;

namespace PoGoEmulator.Requests
{
    public static partial class RequestHandler
    {
        // "POGOProtos.Networking.Envelopes.RequestEnvelope"
        private static void RpcRequestParser(Connection connectedClient, bool isPlfe, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var usr = connectedClient.GetPlayerByRequest();
            if (isPlfe)
            {
                connectedClient.OnRequest();
            }
            else
            {
                throw new Exception("not implemented");
            }
        }
    }
}