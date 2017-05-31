using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using PoGoEmulator.Enums;
using POGOProtos.Networking.Envelopes;
using PoGoEmulator;
using PoGoEmulator.Database.Tables;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace

namespace PoGoEmulator.Models
{
    public class AuthConnection : BaseConnection
    {
        protected string UEmail
        {
            get
            {
                if (base.StreamContext.ProtoRequest.RequestId == 0)
                    throw new Exception("Request doesn't have protobuf data..");

                var authInfo = base.StreamContext.ProtoRequest.AuthInfo;
                if (authInfo.IsNull() || authInfo.Provider.IsNull())
                    throw new Exception("Invalid authentication token! Kicking..");

                JwtSecurityTokenHandler jwth = new JwtSecurityTokenHandler();
                var userJwtToken = jwth.ReadJwtToken(base.StreamContext.ProtoRequest.AuthInfo.Token.Contents).Payload;
                object userEmail;
                userJwtToken.TryGetValue("email", out userEmail);
                if (userEmail.IsNull())
                    throw new Exception("useremail not found");
                return userEmail.ToString();
            }
        }

        protected CacheUserData CurrentPlayer
        {
            get
            {
                CacheUserData state;
                Global.AuthenticatedUsers.TryGetValue(UEmail, out state);
                return state;
            }
        }

        protected bool IsAuth
        {
            get
            {
                if (CurrentPlayer == null)
                    return false;
                else
                    return CurrentPlayer.IsAuthenticated;
            }
        }

        public AuthConnection(TcpClient client, CancellationTokenSource cancelToken) : base(client, cancelToken)
        {
        }

        public override HttpStreamResult Abort(RequestState state, Exception e = null)
        {
            return base.Abort(state, e);
        }

        private void GetAuthTicket()
        {
            ProtoResponse.StatusCode = 53;
            ProtoResponse.RequestId = StreamContext.ProtoRequest.RequestId;
            ProtoResponse.ApiUrl = "pgorelease.nianticlabs.com/custom";
            ProtoResponse.AuthTicket = new AuthTicket()
            {
                Start = ByteString.Empty,
                ExpireTimestampMs = DateTime.Now.ToUnixTime(),
                End = ByteString.Empty,
            };
        }

        protected HttpStreamResult AuthenticatePlayer()
        {
            GetAuthTicket();
            var authInfo = StreamContext.ProtoRequest.AuthInfo;
            if (authInfo.IsNull() || authInfo.Provider.IsNull())
            {
                throw new Exception("Invalid authentication token! Kicking..");
            }

            if (authInfo.Provider == "google")
            {
                UpdateAuthorization();
            }
            else
            {
                throw new Exception("Invalid authentication token! Kicking..");
            }

            //validEmail()

            AddOrUpdateUserLocation();
            return Abort(RequestState.Completed);
        }

        protected void AddOrUpdateUserLocation()
        {
            var email = UEmail;
            var user = Database.Users.FirstOrDefault(p => p.email == email);
            if (user == null)
            {
                //Log.Dbg($"user is notfound adding now");
                user = new User
                {
                    email = email,
                    username = email.Split('@')[0],
                    altitude = StreamContext.ProtoRequest.Altitude,
                    longitude = StreamContext.ProtoRequest.Longitude,
                    latitude = StreamContext.ProtoRequest.Latitude,
                };
                Database.Users.Add(user);
                //Log.Dbg($"user is added: {user.email}");
            }
            else
            {
                user.altitude = StreamContext.ProtoRequest.Altitude;
                user.longitude = StreamContext.ProtoRequest.Longitude;
                user.latitude = StreamContext.ProtoRequest.Latitude;
                Database.Users.Update(user);
                //Log.Dbg($"user is updated: {user.email}");
            }
            Database.SaveChanges();
        }

        private void UpdateAuthorization()
        {
            CacheUserData state = CurrentPlayer;
            if (state != null)
            {
                if (!state.IsAuthenticated)
                {
                    state.IsAuthenticated = true;
                    Global.AuthenticatedUsers.AddOrUpdate(UEmail, state, (k, v) => state);
                }
            }
            else
            {
                state = new CacheUserData()
                {
                    IsAuthenticated = true
                };
                Global.AuthenticatedUsers.AddOrUpdate(UEmail, state, (k, v) => state);
            }
        }
    }
}