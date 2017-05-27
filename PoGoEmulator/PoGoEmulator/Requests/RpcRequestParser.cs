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

            var r = connectedClient.HttpContext;
            if (isPlfe)
            {
                var user = connectedClient.Database.Users.SingleOrDefault(p => p.email == r.UserEmail);
                if (user == null)
                {
                    user = new User
                    {
                        email = r.UserEmail,
                        username = r.UserEmail.Split('@')[0],
                        altitude = r.Request.Altitude,
                        longitude = r.Request.Longitude,
                        latitude = r.Request.Latitude,
                    };
                    connectedClient.Database.Users.Add(user);
                }
                else
                {
                    user.altitude = r.Request.Altitude;
                    user.longitude = r.Request.Longitude;
                    user.latitude = r.Request.Latitude;
                    connectedClient.Database.Users.Update(user);
                }

                if (!r.IsAuthenticated)
                {
                    r.DoAuth();
                    return;//do not remove
                }

                if (!r.Request.Requests.Any())
                {
                    if (r.Request.Unknown6 != null && r.Request.Unknown6.RequestType == 6)
                    {
                        r.Response.Unknown6.Add(new Unknown6Response()
                        {
                            ResponseType = 6,
                            Unknown2 = new Unknown6Response.Types.Unknown2()
                            {
                                Unknown1 = 1
                            }
                        });
                        r.Response.StatusCode = 1;
                        return;
                    }
                }

                foreach (var re in r.Request.Requests)
                {
                    r.ProcessResponse(re);
                    //r.Response.Returns.Add(rew.RequestMessage);
                }
            }
            else
            {
                throw new Exception("not implemented");
            }
        }
    }
}