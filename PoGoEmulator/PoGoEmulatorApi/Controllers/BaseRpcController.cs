using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Google.Protobuf;
using log4net;
using Microsoft.EntityFrameworkCore;
using PoGoEmulatorApi.Database;
using PoGoEmulatorApi.Database.Tables;
using PoGoEmulatorApi.Enums;
using PoGoEmulatorApi.Models;
using POGOProtos.Networking.Envelopes;
using ITraceWriter = System.Web.Http.Tracing.ITraceWriter;

namespace PoGoEmulatorApi.Controllers
{
    public class BaseRpcController : ApiController
    {
        public PoGoDbContext Database { get; set; }
        public ILog Log { get; set; } = null;
        public HttpContext RpcContext { get { return HttpContext.Current; } }
        public HttpResponse Response { get { return RpcContext.Response; } }
        public new HttpRequest Request { get { return RpcContext.Request; } }
        private Exception CurrentException { get; set; }
        private RequestEnvelope _requestProto;

        public HttpRequestMessage HttpRequestMessage { get { return RpcContext.Items["MS_HttpRequestMessage"] as HttpRequestMessage; } }

        public BaseRpcController(PoGoDbContext db)
        {
            this.Database = db;
        }

        public RequestEnvelope ProtoRequest
        {
            get
            {
                LoadProtoContent();
                return _requestProto;
            }
            set { _requestProto = value; }
        }

        public ResponseEnvelope ProtoResponse { get; set; } = new ResponseEnvelope();
        public RpcRequestType RpcType { get; set; } = RpcRequestType.None;

        public virtual HttpResponseMessage Rpc()
        {
            return AnswerToUser(HttpStatusCode.OK);
        }

        protected void LoadProtoContent()
        {
            if (_requestProto != null) return;
            if (RpcType == RpcRequestType.None) return;

            _requestProto = new RequestEnvelope();
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);

            Byte[] buf = new byte[req.Length];
            req.Read(buf, 0, buf.Length);
            Google.Protobuf.CodedInputStream cis = new CodedInputStream(buf);
            _requestProto.MergeFrom(cis);
        }

        public HttpResponseMessage ThrowException(Exception e)
        {
            CurrentException = e;
            Log?.Error("", e);
            //dispose ALL
            Database.Dispose();
            return AnswerToUser(HttpStatusCode.BadRequest);
        }

        protected HttpResponseMessage AnswerToUser(HttpStatusCode code)
        {
            HttpResponseMessage res = new HttpResponseMessage(code);
            res.Headers.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
            res.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            res.Headers.TryAddWithoutValidation("PoGoEmulator", ".NET 4.6.2 MVC API");
            res.Headers.TryAddWithoutValidation("Date", $"{string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT");
            if (code == HttpStatusCode.OK && CurrentException == null)
            {
                res.Content = new ByteArrayContent(ProtoResponse.ToByteArray());
                Database.SaveChanges();
                Log.Dbg($"succesfully responding");
            }
            else
            {
                Log.Dbg($"unsuccesfully responding error: {CurrentException.Message}");
                res.Content = new StringContent(CurrentException.Message);
            }
            return res;
        }

        private string _useremail;

        public string UserEmail
        {
            get
            {
                if (ProtoRequest?.AuthInfo == null)
                {
                    Log.Dbg($"Error: ProtoRequest.AuthInfo is NULL");
                    return "";
                }
                if (Request == null || !ProtoRequest.AuthInfo.Token.Contents.Any())
                {
                    Log.Dbg($"Error: Request is NULL or token contents is NULL");
                    return "";
                }
                else
                {
                    if (_useremail == null)
                    {
                        Log.Dbg($"_useremail is null ");
                        JwtSecurityTokenHandler jwth = new JwtSecurityTokenHandler();
                        var userJwtToken = jwth.ReadJwtToken(ProtoRequest.AuthInfo.Token.Contents).Payload;
                        object userEmail;
                        userJwtToken.TryGetValue("email", out userEmail);
                        _useremail = userEmail?.ToString().ToLower();
                        Log.Dbg($"_useremail is set to {_useremail}");
                    }
                    return _useremail;
                }
            }
        }

        public CacheUserData CachedCurrentUser
        {
            get
            {
                CacheUserData state;
                WebApiApplication.AuthenticatedUsers.TryGetValue(UserEmail, out state);
                Log.Dbg($"is CachedCurrentUser NULL?: {state == null}");
                return state;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                CacheUserData state = CachedCurrentUser;
                bool r = CachedCurrentUser != null && state.IsAuthenticated;
                Log.Dbg($"Is Authenticated?: {r}");
                return r;
            }
        }

        public void UpdatePlayerLocation()
        {
            if (UserEmail == null)
                throw new Exception("detected->USER NOT FOUND");

            var user = Database.Users.FirstOrDefault(p => p.email == UserEmail);
            if (user == null)
            {
                Log.Dbg($"user is null adding now");
                user = new User
                {
                    email = UserEmail,
                    username = UserEmail.Split('@')[0],
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

        public void UpdateCachedUser()
        {
            var oauth = CachedCurrentUser;
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
            WebApiApplication.AuthenticatedUsers.AddOrUpdate(UserEmail, oauth, (k, v) => oauth);
            Log.Dbg($"CachedCurrentUser is updated: {CachedCurrentUser.IsAuthenticated}");
        }
    }
}