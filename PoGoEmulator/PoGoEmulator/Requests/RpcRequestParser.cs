using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using PoGoEmulator.Database.Tables;
using PoGoEmulator.Logging;
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