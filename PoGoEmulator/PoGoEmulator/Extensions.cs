﻿using Google.Protobuf;
using HttpMachine;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
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
                var httpParser = new HttpParser(handler);
                var buffer = new byte[Global.Cfg.MaxRequestContentLength];

                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                Array.Resize(ref buffer, bytesRead);
                var d = httpParser.Execute(new ArraySegment<byte>(buffer, 0, bytesRead));
                if (bytesRead != d)
                    throw new Exception("data not matching");

                //// ensure you get the last callbacks.
                //httpParser.Execute(default(ArraySegment<byte>));

                //if (handler.Request == null)
                //Logger.Write(Encoding.Default.GetString(buffer), LogLevel.Response);
                return handler;
            }
            catch (Exception e)
            {
                Logger.Write(e);
                throw;
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
        /// successful response send s 
        /// </summary>
        /// <param name="ns">
        /// active connection 
        /// </param>
        /// <param name="responseToUser">
        /// configured user data 
        /// </param>
        public static void WriteProtoResponse(this NetworkStream ns, ResponseEnvelope responseToUser)//NOT TESTED FUNCTION
        {
            ns.WriteHttpResponse(responseToUser.ToByteString());
        }

        /// <summary>
        /// error sends 
        /// </summary>
        /// <param name="ns">
        /// active connection 
        /// </param>
        /// <param name="statusCode">
        /// selet only unsuccessful statusCodes 
        /// </param>
        /// <param name="errorMessage">
        /// error message 
        /// </param>
        public static void WriteProtoResponse(this NetworkStream ns, HttpStatusCode statusCode, string errorMessage)
        {
            var responseToUser = new ResponseEnvelope
            {
                StatusCode = (int)statusCode,
                Error = errorMessage
            };
            ns.WriteHttpResponse(responseToUser.ToByteString());
        }

        public static void WriteHttpResponse(this NetworkStream ns, ByteString body)
        {
            var header = new StringBuilder();
            Global.DefaultResponseHeader.ToList().ForEach(item => header.AppendLine($"{item.Key}: {item.Value}"));
            header.AppendLine($"Date: {string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT");
            header.AppendLine("Content-Length: " + (body.Length + header.Length + 2));
            header.AppendLine("");
            var writer = new StreamWriter(ns);
            writer.WriteLine(header.ToString());
            writer.Write(body.ToByteArray());
            writer.Flush();
        }

        public static ulong ToUnixTime(this DateTime dt, TimeSpan ts)
        {
            var timeSpan = (dt.Add(ts).ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (ulong)timeSpan.TotalSeconds * 1000;
        }
    }
}