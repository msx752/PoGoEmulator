using Google.Protobuf;
using HttpMachine;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using PoGoEmulator.Requests;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator
{
    public static class Extensions
    {
        /// <summary>
        /// HTTPCONTEXT generator 
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <param name="ct">
        /// </param>
        /// <param name="checkUserAuthentication">
        /// </param>
        /// <returns>
        /// </returns>
        public static MyHttpContext GetContext(this NetworkStream stream, CancellationToken ct, bool checkUserAuthentication)
        {
            try
            {
                var handler = new MyHttpContext(checkUserAuthentication);
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

            return serverResponse;
        }

        public static T[] ToArray<T>(this ArraySegment<T> arraySegment)
        {
            T[] array = new T[arraySegment.Count];
            Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
            return array;
        }

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

        /// <summary>
        /// response to user 
        /// </summary>
        /// <param name="ns">
        /// </param>
        /// <param name="responseToUser">
        /// </param>
        public static void WriteProtoResponse(this NetworkStream ns, ResponseEnvelope responseToUser)//NOT TESTED FUNCTION
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(responseToUser.ToByteString().ToByteArray())
            };
            Global.DefaultResponseHeader.ToList().ForEach(item => response.Headers.Add(item.Key, item.Value));
            var rtask = response.Content.ReadAsByteArrayAsync();
            rtask.Wait();
            ns.Write(rtask.Result, 0, rtask.Result.Length);
            ns.Flush();
        }

        public static void WriteBadRequest(this NetworkStream ns, HttpStatusCode code, string message)
        {
            HttpResponseMessage responseToUser = new HttpResponseMessage(code)
            {
                Content = new StringContent(message)
            };
            Global.DefaultResponseHeader.ToList().ForEach(item => responseToUser.Headers.Add(item.Key, item.Value));
            var rtask = responseToUser.Content.ReadAsByteArrayAsync();
            rtask.Wait();
            ns.Write(rtask.Result, 0, rtask.Result.Length);
            ns.Flush();
        }
    }
}