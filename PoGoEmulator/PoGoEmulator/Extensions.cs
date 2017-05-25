using Google.Protobuf;
using HttpMachine;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using PoGoEmulator.Requests;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator
{
    public static class Extensions
    {
        public static MyHttpContext GetContext(this NetworkStream stream, CancellationToken ct)
        {
            try
            {
                var handler = new MyHttpContext();
                var parser = new HttpParser(handler);

                var buffer = new byte[Global.Cfg.MaxRequestContentLength];

                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                Array.Resize(ref buffer, bytesRead);
                int d = parser.Execute(new ArraySegment<byte>(buffer, 0, bytesRead));
                if (bytesRead != d)
                {
                    throw new Exception("data not matching");
                }
                // ensure you get the last callbacks.
                parser.Execute(default(ArraySegment<byte>));

                return handler;
            }
            catch (Exception e)
            {
                Logger.Write(e);
                return null;
            }
        }

        public static string JoinLines(this Dictionary<string, string> headers)
        {
            return string.Join(Environment.NewLine, headers.Select(x => x.Key + ": " + x.Value).ToArray()) + Environment.NewLine;
        }

        //not necessary
        //public static T Proton<T>(this Connection cnnUser) where T : class
        //{
        //    var serverResponse = Proton<T>(cnnUser.HttpContext.body.First());
        //    return serverResponse;
        //}

        /// <summary>
        /// protobuf file deserialise on pure byte[] file , (becareful object must be a type of proto )
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="protobuf">
        /// </param>
        /// <param name="checkAuthentication">
        /// DISABLE IT FOR LOCAL SERIALIZING 
        /// </param>
        /// <returns>
        /// </returns>
        public static T Proton<T>(this Byte[] protobuf, bool checkAuthentication = true) where T : class
        {
            CodedInputStream codedStream = new CodedInputStream(protobuf);
            T serverResponse = Activator.CreateInstance(typeof(T)) as T;
            MethodInfo methodMergeFrom = serverResponse?.GetType().GetMethods().ToList()
                .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");
            if (methodMergeFrom == null)
                throw new Exception("undefined protobuf class");
            methodMergeFrom.Invoke(serverResponse, new object[] { codedStream });

            if (checkAuthentication)//for user requests
            {
                var requestAuthInfo = serverResponse.GetType().GetProperties().ToList()
                    .FirstOrDefault(p => p.ToString() == "AuthInfo AuthInfo");
                GoogleRequest.IsValidToken(requestAuthInfo.GetValue(serverResponse).Cast<RequestEnvelope.Types.AuthInfo>());
            }
            return serverResponse;
        }

        public static T[] ToArray<T>(this ArraySegment<T> arraySegment)
        {
            T[] array = new T[arraySegment.Count];
            Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
            return array;
        }

        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }
    }
}