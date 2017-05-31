using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using Google.Protobuf;
using PoGoEmulatorApi.Database.Tables;
using PoGoEmulatorApi.Models;
using POGOProtos.Networking.Envelopes;

// ReSharper disable once CheckNamespace
namespace PoGoEmulatorApi.Controllers
{
    public class AuthorizationController3 : ResponseController2
    {
        public AuthorizationController3(PoGoDbContext db) : base(db)
        {
        }

        protected CacheUserData CurrentPlayer
        {
            get
            {
                CacheUserData state;
                WebApiApplication.AuthenticatedUsers.TryGetValue(UEmail, out state);
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

        protected string UEmail
        {
            get
            {
                var authInfo = ProtoRequest.AuthInfo;
                if (authInfo.IsNull() || authInfo.Provider.IsNull())
                    throw new Exception("Invalid authentication token! Kicking..");

                JwtSecurityTokenHandler jwth = new JwtSecurityTokenHandler();
                var userJwtToken = jwth.ReadJwtToken(ProtoRequest.AuthInfo.Token.Contents).Payload;
                object userEmail;
                userJwtToken.TryGetValue("email", out userEmail);
                if (userEmail.IsNull())
                    throw new Exception("useremail not found");
                return userEmail.ToString();
            }
        }

        [System.Web.Http.NonAction]
        protected HttpResponseMessage AuthenticatePlayer()
        {
            try
            {
                GetAuthTicket();
                var authInfo = ProtoRequest.AuthInfo;
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
                return base.ResponseToClient(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [System.Web.Http.NonAction]
        protected void UpdateAuthorization()
        {
            CacheUserData state = CurrentPlayer;
            if (state != null)
            {
                if (!state.IsAuthenticated)
                {
                    state.IsAuthenticated = true;
                    WebApiApplication.AuthenticatedUsers.AddOrUpdate(UEmail, state, (k, v) => state);
                }
            }
            else
            {
                state = new CacheUserData()
                {
                    IsAuthenticated = true
                };
                WebApiApplication.AuthenticatedUsers.AddOrUpdate(UEmail, state, (k, v) => state);
            }
        }

        [System.Web.Http.NonAction]
        protected bool UpdateCurrentPlayer(CacheUserData updatedData)
        {
            return WebApiApplication.AuthenticatedUsers.AddOrUpdate(UEmail, updatedData, (k, v) => updatedData) != null;
        }

        [System.Web.Http.NonAction]
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
                    altitude = ProtoRequest.Altitude,
                    longitude = ProtoRequest.Longitude,
                    latitude = ProtoRequest.Latitude,
                };
                Database.Users.Add(user);
                //Log.Dbg($"user is added: {user.email}");
            }
            else
            {
                user.altitude = ProtoRequest.Altitude;
                user.longitude = ProtoRequest.Longitude;
                user.latitude = ProtoRequest.Latitude;
                Database.Users.Update(user);
                //Log.Dbg($"user is updated: {user.email}");
            }
            Database.SaveChanges();
        }

        [System.Web.Http.NonAction]
        private void GetAuthTicket()
        {
            ProtoResponse.StatusCode = 53;
            ProtoResponse.RequestId = ProtoRequest.RequestId;
            ProtoResponse.ApiUrl = "pgorelease.nianticlabs.com/custom";
            ProtoResponse.AuthTicket = new AuthTicket()
            {
                Start = ByteString.Empty,
                ExpireTimestampMs = DateTime.Now.ToUnixTime(),
                End = ByteString.Empty,
            };
        }
    }
}