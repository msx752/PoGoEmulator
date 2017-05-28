using HttpMachine;
using POGOProtos.Networking.Envelopes;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using PoGoEmulator.Requests;

namespace PoGoEmulator.Models
{
    /// <summary>
    /// https://github.com/bvanderveen/httpmachine 
    /// </summary>
    public class MyHttpContext : IHttpParserHandler
    {
        public MyHttpContext(bool checkUserAuthentication)
        {
            CheckUserAuth = checkUserAuthentication;
        }

        public bool CheckUserAuth { get; private set; }
        public List<byte[]> Body { get; set; } = new List<byte[]>();
        public string Fragment { get; set; }
        public string HeaderName { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        public string HeaderValue { get; set; }
        public string Method { get; set; }
        public bool OnHeadersEndCalled { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestUri { get; set; }
        public bool ShouldKeepAlive { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string StatusReason { get; set; }
        public int VersionMajor { get; set; } = -1;
        public int VersionMinor { get; set; } = -1;

        // <summary>
        // authenticated userEmail 
        // </summary>
        public string UserEmail
        {
            get
            {
                if (Request == null || !Request.AuthInfo.Token.Contents.Any())
                    return null;
                else
                {
                    JwtSecurityTokenHandler jwth = new JwtSecurityTokenHandler();
                    var userJwtToken = jwth.ReadJwtToken(Request.AuthInfo.Token.Contents).Payload;
                    object userEmail;
                    userJwtToken.TryGetValue("email", out userEmail);
                    return userEmail?.ToString().ToLower();
                }
            }
        }

        public CacheUserData CachedUserData
        {
            get
            {
                CacheUserData state;
                Global.AuthenticatedUsers.TryGetValue(UserEmail, out state);
                return state;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                CacheUserData state = CachedUserData;
                return CachedUserData != null && state.IsAuthenticated;
            }
        }

        /// <summary>
        /// request from user 
        /// </summary>
        public RequestEnvelope Request { get; private set; }

        /// <summary>
        /// configure it for response to user 
        /// </summary>
        public ResponseEnvelope Response { get; private set; } = new ResponseEnvelope();

        public void OnBody(HttpParser parser, ArraySegment<byte> data)
        {
            //remove the pure data after the serializing but now it's ok.
            Body.Add(data.ToArray());
            Request = Body.First().Proton<RequestEnvelope>();
        }

        public void OnFragment(HttpParser parser, string fragment)
        {
            this.Fragment = fragment;
        }

        public void OnHeaderName(HttpParser parser, string name)
        {
            if (!string.IsNullOrEmpty(HeaderValue))
                CommitHeader();

            HeaderName = name;
        }

        public void OnHeadersEnd(HttpParser parser)
        {
            OnHeadersEndCalled = true;

            if (!string.IsNullOrEmpty(HeaderValue))
                CommitHeader();

            VersionMajor = parser.MajorVersion;
            VersionMinor = parser.MinorVersion;
            ShouldKeepAlive = parser.ShouldKeepAlive;
        }

        public void OnHeaderValue(HttpParser parser, string value)
        {
            if (string.IsNullOrEmpty(HeaderName))
                throw new Exception("Got header value without name.");

            HeaderValue = value;
        }

        public void OnMessageBegin(HttpParser parser)
        {
        }

        public void OnMethod(HttpParser parser, string method)
        {
            this.Method = method;
        }

        public void OnQueryString(HttpParser parser, string queryString)
        {
            this.QueryString = queryString;
        }

        public void OnRequestUri(HttpParser parser, string requestUri)
        {
            this.RequestUri = requestUri;
        }

        private void CommitHeader()
        {
            //Console.WriteLine("Committing header '" + headerName + "' : '" + headerValue + "'");
            Headers[HeaderName] = HeaderValue;
            HeaderName = HeaderValue = null;
        }

        public void OnMessageEnd(HttpParser parser)
        {
            if (Request == null)
                throw new Exception("request body is empty");

            if (CheckUserAuth)//for user requests (every request will check whether authed or not)
                GoogleRequest.CheckUserValidToken(Request.AuthInfo);

            StatusCode = HttpStatusCode.OK;//do not change this
        }
    }
}