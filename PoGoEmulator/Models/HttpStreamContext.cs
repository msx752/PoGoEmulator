using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using HttpMachine;
using PoGoEmulator.Requests;
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
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public string StatusReason { get; set; }
        public int VersionMajor { get; set; } = -1;
        public int VersionMinor { get; set; } = -1;

        /// <summary>
        /// request from user 
        /// </summary>
        public RequestEnvelope ProtoRequest { get; private set; }

        public void OnBody(HttpParser parser, ArraySegment<byte> data)
        {
            ProtoRequest = ProtoCaster<RequestEnvelope>(data.ToArray());
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

        /// <summary>
        /// protobuf file deserialise on pure byte[] file , (becareful object must be a type of proto )
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="protobuf">
        /// </param>
        /// <returns>
        /// </returns>
        public T ProtoCaster<T>(Byte[] protobuf) where T : class
        {
            CodedInputStream codedStream = new CodedInputStream(protobuf);
            T serverResponse = Activator.CreateInstance(typeof(T)) as T;
            MethodInfo methodMergeFrom = serverResponse?.GetType().GetMethods().ToList()
                .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");
            if (methodMergeFrom == null)
                throw new Exception("undefined protobuf class");
            methodMergeFrom.Invoke(serverResponse, new object[] { codedStream });

            return serverResponse;
        }
    }
}