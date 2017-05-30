using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Database.Tables;
using PoGoEmulatorApi.Models;
using PoGoEmulatorApi.Responses;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulatorApi.Controllers
{
    public class AuthorizedController : BaseRpcController
    {
        public AuthorizedController(PoGoDbContext db) : base(db)
        {
        }

        public string UEmail
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

        public CacheUserData CurrentPlayer
        {
            get
            {
                CacheUserData state;
                WebApiApplication.AuthenticatedUsers.TryGetValue(UEmail, out state);
                return state;
            }
        }

        public bool IsAuth
        {
            get
            {
                if (CurrentPlayer == null)
                    return false;
                else
                    return CurrentPlayer.IsAuthenticated;
            }
        }

        public HttpResponseMessage AuthenticatePlayer()
        {
            try
            {
                GetAuthTicket();
                var authInfo = ProtoRequest.AuthInfo;
                if (authInfo.IsNull() || authInfo.Provider.IsNull())
                {
                    return this.ThrowException(new Exception("Invalid authentication token! Kicking.."));
                }

                if (authInfo.Provider == "google")
                {
                    UpdateAuthorization();
                }
                else
                {
                    return this.ThrowException(new Exception("Invalid authentication token! Kicking.."));
                }

                //validEmail()

                AddOrUpdateUser(UEmail);
                return base.Rpc();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

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

        protected bool UpdateCurrentPlayer(CacheUserData updatedData)
        {
            return WebApiApplication.AuthenticatedUsers.AddOrUpdate(UEmail, updatedData, (k, v) => updatedData) != null;
        }

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

        private void AddOrUpdateUser(string email)
        {
            var user = Database.Users.FirstOrDefault(p => p.email == email);
            if (user == null)
            {
                Log.Dbg($"user is notfound adding now");
                user = new User
                {
                    email = email,
                    username = email.Split('@')[0],
                    altitude = ProtoRequest.Altitude,
                    longitude = ProtoRequest.Longitude,
                    latitude = ProtoRequest.Latitude,
                };
                Database.Users.Add(user);
                Log.Dbg($"user is added: {user.email}");
            }
            else
            {
                user.altitude = ProtoRequest.Altitude;
                user.longitude = ProtoRequest.Longitude;
                user.latitude = ProtoRequest.Latitude;
                Database.Users.Update(user);
                Log.Dbg($"user is updated: {user.email}");
            }
            Database.SaveChanges();
        }

        public HttpResponseMessage OnRequest()
        {
            try
            {
                if (!IsAuth)
                {
                    return AuthenticatePlayer(); ;
                }

                Log.Debug($"HasSignature:{CurrentPlayer.HasSignature}");
                if (CurrentPlayer.HasSignature == false)
                {
                    if (ProtoResponse.Unknown6 != null)
                    {
                        //POGOProtos.Networking.Envelopes.Signature
                        //connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature
                        var signature = Encryption.Decrypt(
                               ProtoRequest.Unknown6.Unknown2.EncryptedSignature.ToByteArray());
                        var codedStream = new CodedInputStream(signature);
                        var sig = new Signature();
                        sig.MergeFrom(codedStream);
                        if (sig.DeviceInfo != null)
                        {
                            var usrd = CurrentPlayer;
                            usrd.HasSignature = true;
                            usrd.IsIOS = (sig.DeviceInfo.DeviceBrand == "Apple");
                            bool updtrslt = UpdateCurrentPlayer(usrd);
                            if (!updtrslt)
                            {
                                return this.ThrowException(new Exception(" CONCURRENT ACCESS ERROR this shouldn't happen"));
                            }
                        }
                    }
                }

                Log.Debug($"HasSignature:{CurrentPlayer.HasSignature}, Platform:{CurrentPlayer.Platform}");
                Log.Debug($"ProtoRequest.Requests.Count:{ProtoRequest.Requests.Count}");
                if (ProtoRequest.Requests.Count == 0)
                {
                    if (ProtoRequest.Unknown6 != null && ProtoRequest.Unknown6.RequestType == 6)
                    {
                        Log.Debug($"ProtoRequest.Unknown6.RequestType:{ProtoRequest.Unknown6.RequestType}");
                        return this.EnvelopResponse();
                    }
                    else
                    {
                        return this.ThrowException(new Exception("Invalid Request!."));
                    }
                }
                RepeatedField<ByteString> requests = this.ProcessRequests();
                return this.EnvelopResponse(requests);
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }
    }
}