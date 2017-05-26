using HttpMachine;
using POGOProtos.Networking.Envelopes;
using System;
using System.Collections.Generic;
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
        public MyHttpContext()
        {
            ResponseProto = new ResponseEnvelope();
        }

        public MyHttpContext(bool checkUserAuthentication) : this()
        {
            CheckUserAuth = checkUserAuthentication;
        }

        public bool CheckUserAuth { get; private set; }
        public List<byte[]> Body { get; set; }
        public string Fragment { get; set; }
        public string HeaderName { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string HeaderValue { get; set; }
        public string Method { get; set; }
        public bool OnHeadersEndCalled { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestUri { get; set; }
        public bool ShouldKeepAlive { get; set; }
        public int? StatusCode { get; set; }
        public string StatusReason { get; set; }
        public int VersionMajor { get; set; } = -1;
        public int VersionMinor { get; set; } = -1;

        /// <summary>
        /// request from user 
        /// </summary>
        public RequestEnvelope RequestProto { get; set; }

        /// <summary>
        /// response to user 
        /// </summary>
        public ResponseEnvelope ResponseProto { get; set; }

        public void OnBody(HttpParser parser, ArraySegment<byte> data)
        {
            //remove the pure data after the serializing but now it's ok.
            Body.Add(data.ToArray());
            RequestProto = Body.First().Proton<RequestEnvelope>();
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
            Headers = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            Body = new List<byte[]>();
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

        /// <summary>
        /// end trigger 
        /// </summary>
        /// <param name="parser">
        /// </param>
        public void OnMessageEnd(HttpParser parser)
        {
            if (RequestProto == null)
                throw new Exception("request body is empty");

            if (CheckUserAuth)//for user requests (every request will check whether authed or not)
                GoogleRequest.CheckUserValidToken(RequestProto.AuthInfo);

            StatusCode = (int)HttpStatusCode.OK;//do not change this
        }
    }
}