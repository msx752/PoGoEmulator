using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using Microsoft.IdentityModel.Tokens;

namespace PoGoEmulator.Requests
{
    public static partial class RequestHandler
    {
        // "POGOProtos.Networking.Envelopes.RequestEnvelope"
        private static void RpcRequestParser(Connection connectedClient, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            RequestEnvelope rqs = connectedClient.HttpContext.Body.First().Proton<RequestEnvelope>();
        }
    }
}