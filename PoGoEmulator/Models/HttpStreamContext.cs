using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using HttpMachine;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator.Models
{
    public class HttpStreamContext : IHttpParserHandler
    {
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public string Fragment { get; set; }
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
        public string Method { get; set; }
        public bool OnHeadersEndCalled { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestUri { get; set; }
        public bool ShouldKeepAlive { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusReason { get; set; }
        public int VersionMajor { get; set; } = -1;
        public int VersionMinor { get; set; } = -1;

        public Uri Url
        {
            get
            {
                return new Uri("http://PoGoEmulator" + this.RequestUri); ;
            }
        }

        /// <summary>
        /// request from user 
        /// </summary>
        public RequestEnvelope ProtoRequest { get; } = new RequestEnvelope();

        public void OnBody(HttpParser parser, ArraySegment<byte> data)
        {
            CodedInputStream codedStream = new CodedInputStream(data.ToArray());
            ProtoRequest.MergeFrom(codedStream);
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
            if (ProtoRequest == null)
                StatusCode = HttpStatusCode.BadRequest;
            else
                StatusCode = HttpStatusCode.OK;
        }
    }
}