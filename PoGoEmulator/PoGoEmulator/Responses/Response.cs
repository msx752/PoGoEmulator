using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using PoGoEmulator.Database.Tables;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;

namespace PoGoEmulator.Responses
{
    public static class Extensions
    {
        public static bool PlayerIsRegistered()
        {
            return false;
        }

        public static void AuthenticatePlayer(this MyHttpContext context)
        {
            context.Response.StatusCode = 53;
            context.Response.RequestId = context.Request.RequestId;
            context.Response.ApiUrl = "pgorelease.nianticlabs.com/custom";
            context.Response.AuthTicket = new AuthTicket()
            {
                Start = ByteString.Empty,
                ExpireTimestampMs = DateTime.Now.UnixTime(new TimeSpan(0, 30, 0)),
                End = ByteString.Empty
            };
            var oauth = context.CachedUserData;
            if (oauth != null)
            {
                oauth.IsAuthenticated = true;
            }
            else
            {
                oauth = new CacheUserData()
                {
                    IsAuthenticated = true
                };
            }
            Global.AuthenticatedUsers.AddOrUpdate(context.UserEmail, oauth, (k, v) => oauth);
        }

        public static User GetPlayerByRequest(this Connection connectedClient)
        {
            var user = connectedClient.Database.Users.SingleOrDefault(p => p.email == connectedClient.HttpContext.UserEmail);
            if (user == null)
            {
                user = new User
                {
                    email = connectedClient.HttpContext.UserEmail,
                    username = connectedClient.HttpContext.UserEmail.Split('@')[0],
                    altitude = connectedClient.HttpContext.Request.Altitude,
                    longitude = connectedClient.HttpContext.Request.Longitude,
                    latitude = connectedClient.HttpContext.Request.Latitude,
                };
                connectedClient.Database.Users.Add(user);
            }
            else
            {
                user.altitude = connectedClient.HttpContext.Request.Altitude;
                user.longitude = connectedClient.HttpContext.Request.Longitude;
                user.latitude = connectedClient.HttpContext.Request.Latitude;
                connectedClient.Database.Users.Update(user);
            }
            return user;
        }

        public static void OnRequest(this Connection connectedClient)
        {
            var context = connectedClient.HttpContext;
            if (!context.IsAuthenticated)
            {
                context.AuthenticatePlayer();
                return;//request ended
            }

            if (connectedClient.HttpContext.CachedUserData.HasSignature == false)
            {
                if (connectedClient.HttpContext.Request.Unknown6 != null)
                {
                    //POGOProtos.Networking.Envelopes.Signature
                    //connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature
                    var signature = Encryption.Decrypt(
                           connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature.ToByteArray());

                    var codedStream = new CodedInputStream(signature);
                    var sig = new Signature();
                    sig.MergeFrom(codedStream);
                    if (sig.DeviceInfo != null)
                    {
                        var usrd = connectedClient.HttpContext.CachedUserData;
                        usrd.HasSignature = true;
                        usrd.IsIOS = (sig.DeviceInfo.DeviceBrand == "Apple");
                        bool updtrslt = Global.AuthenticatedUsers.TryUpdate(connectedClient.HttpContext.UserEmail, usrd,
                             connectedClient.HttpContext.CachedUserData);
                        if (!updtrslt)
                        {
                            throw new Exception(" CONCURRENT ACCESS ERROR this shouldn't happen");
                        }
#if DEBUG
                        var devc = usrd.IsIOS ? "Apple" : "Android";
                        Logger.Write($"User:{connectedClient.HttpContext.UserEmail} connected with {devc} device.", Enums.LogLevel.Debug);
#endif
                    }
                }
            }

            if (!context.Request.Requests.Any())
            {
                if (context.Request.Unknown6 != null && context.Request.Unknown6.RequestType == 6)
                {
                    context.Response.Unknown6.Add(new Unknown6Response()
                    {
                        ResponseType = 6,
                        Unknown2 = new Unknown6Response.Types.Unknown2()
                        {
                            Unknown1 = 1
                        }
                    });
                    context.Response.StatusCode = 1;
                    return;
                }
            }

            foreach (var re in context.Request.Requests)
            {
                context.ProcessResponse(re);
                //r.Response.Returns.Add(rew.RequestMessage);
            }
        }

        public static void ProcessResponse(this MyHttpContext context, Request rq)
        {
            var type = rq.RequestType;
            CodedInputStream codedStream = new CodedInputStream(rq.RequestMessage.ToByteArray());
            var strType = $"POGOProtos.Networking.Requests.Messages.{type}Message";
            var tst = (IMessage)Activator.CreateInstance("POGOProtos.dll", strType);
            MethodInfo methodMergeFrom = tst?.GetType().GetMethods().ToList()
                .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");
            methodMergeFrom.Invoke(tst, new object[] { codedStream });

            switch (type)
            {
            }
        }
    }
}