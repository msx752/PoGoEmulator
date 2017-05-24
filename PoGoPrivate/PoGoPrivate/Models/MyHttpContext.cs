using HttpMachine;
using System;
using System.Collections.Generic;

namespace PoGoPrivate.Models
{
    /// <summary>
    /// https://github.com/bvanderveen/httpmachine 
    /// </summary>
    public class MyHttpContext : IHttpParserHandler
    {
        public string method, requestUri, path, queryString, fragment, headerName, headerValue, statusReason;
        public int versionMajor = -1, versionMinor = -1;
        public int? statusCode;
        public Dictionary<string, string> headers;
        public List<byte[]> body;
        public bool onHeadersEndCalled, shouldKeepAlive;

        public void OnMessageBegin(HttpParser parser)
        {
            headers = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            body = new List<byte[]>();
        }

        public void OnMethod(HttpParser parser, string method)
        {
            this.method = method;
        }

        public void OnRequestUri(HttpParser parser, string requestUri)
        {
            this.requestUri = requestUri;
        }

        public void OnFragment(HttpParser parser, string fragment)
        {
            this.fragment = fragment;
        }

        public void OnQueryString(HttpParser parser, string queryString)
        {
            this.queryString = queryString;
        }

        public void OnHeaderName(HttpParser parser, string name)
        {
            //Console.WriteLine("OnHeaderName:  '" + str + "'");

            if (!string.IsNullOrEmpty(headerValue))
                CommitHeader();

            headerName = name;
        }

        private void CommitHeader()
        {
            //Console.WriteLine("Committing header '" + headerName + "' : '" + headerValue + "'");
            headers[headerName] = headerValue;
            headerName = headerValue = null;
        }

        public void OnHeaderValue(HttpParser parser, string value)
        {
            //Console.WriteLine("OnHeaderValue:  '" + str + "'");

            if (string.IsNullOrEmpty(headerName))
                throw new Exception("Got header value without name.");

            headerValue = value;
        }

        public void OnHeadersEnd(HttpParser parser)
        {
            onHeadersEndCalled = true;

            if (!string.IsNullOrEmpty(headerValue))
                CommitHeader();

            versionMajor = parser.MajorVersion;
            versionMinor = parser.MinorVersion;
            shouldKeepAlive = parser.ShouldKeepAlive;
        }

        public void OnBody(HttpParser parser, ArraySegment<byte> data)
        {
            body.Add(data.ToArray());
        }

        public void OnMessageEnd(HttpParser parser)
        {
            // Console.WriteLine("OnMessageEnd");
        }
    }
}