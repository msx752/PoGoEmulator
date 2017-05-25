using HttpMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoGoEmulator.Models
{
    /// <summary>
    /// https://github.com/bvanderveen/httpmachine 
    /// </summary>
    public class MyHttpContext : IHttpParserHandler
    {
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

        public void OnBody(HttpParser parser, ArraySegment<byte> data)
        {
            //remove the pure data after the serializing but now it's ok.
            Body.Add(data.ToArray());
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

        /// <summary>
        /// end trigger 
        /// </summary>
        /// <param name="parser">
        /// </param>
        public void OnMessageEnd(HttpParser parser)
        {
            if (!Body.Any())
                throw new Exception("request body is empty");

            //can i check authorize in here ???
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
    }
}