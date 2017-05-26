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
        private static void RpcRequestParser(Connection connectedClient, bool isPlfe, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var rqs = connectedClient.HttpContext;
            // rqs.ResponseEnvelope //do something
        }
    }
}