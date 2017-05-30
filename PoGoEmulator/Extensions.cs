using Google.Protobuf;
using HttpMachine;
using PoGoEmulator.Models;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator
{
    public static class Extensions
    {
        /// <summary>
        /// global caster 
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        public static MyHttpContext GetContext(this HttpNetworkStream stream, bool checkUserAuthentication)
        {
            try
            {
                var handler = new MyHttpContext(checkUserAuthentication);
                var httpParser = new HttpParser(handler);
                var buffer = stream.ReadBuffer(Global.Cfg.MaxRequestContentLength);
                var d = httpParser.Execute(new ArraySegment<byte>(buffer, 0, buffer.Length));
                if (buffer.Length != d)
                    throw new Exception("data not matching");

                return handler;
            }
            catch (Exception e)
            {
                throw e;
            }
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
        public static T Proton<T>(this Byte[] protobuf) where T : class
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

        public static T[] ToArray<T>(this ArraySegment<T> arraySegment)
        {
            T[] array = new T[arraySegment.Count];
            Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
            return array;
        }

        public static ulong ToUnixTime(this DateTime datetime, TimeSpan ts)
        {
            DateTime dt = DateTime.UtcNow;
            dt = dt.Add(ts);
            var timeSpan = (dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (ulong)timeSpan.TotalSeconds * 1000;
        }

        public static void WriteHttpResponse(this HttpNetworkStream ns, ByteString responseBody)
        {
            var responseString =
                "HTTP/1.1 200 OK\r\n" +
                $"Content-Length: {responseBody.Length}\r\n" +
                "Connection: keep-alive" +
                "\r\n";

            //var responseHeader = new StringBuilder();
            //responseHeader.AppendLine("HTTP/1.1 200 OK");
            //Global.DefaultResponseHeader.ToList().ForEach(item => responseHeader.AppendLine($"{item.Key}: {item.Value}"));
            //responseHeader.AppendLine($"Date: {string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT");
            //responseHeader.AppendLine($"Content-Length: {responseBody.Length}");
            //responseHeader.AppendLine("");

            ns.Write(responseString);
            ns.Write(responseBody.ToArray());
            ns.SendHttpResponse();
        }

        public static void WriteProtoResponse(this HttpNetworkStream ns, ResponseEnvelope responseToUser)
        {
            ns.WriteHttpResponse(responseToUser.ToByteString());
        }

        public static void WriteProtoResponse(this HttpNetworkStream ns, HttpStatusCode statusCode, string errorMessage)
        {
            var responseToUser = new ResponseEnvelope
            {
                StatusCode = (int)statusCode,
                Error = errorMessage
            };
            ns.WriteHttpResponse(responseToUser.ToByteString());
        }
    }
}