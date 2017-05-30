using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Tracing;
using Google.Protobuf;
using log4net;
using PoGoEmulatorApi.Controllers;

namespace PoGoEmulatorApi
{
    public static class Extensions
    {      /// <summary>
           /// protobuf file deserialise on pure byte[] file , (becareful object must be a type of
           /// proto ) </summary> <typeparam name="T"> </typeparam> <param name="protobuf"> </param>
           /// <returns> </returns>
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

        public static ulong ToUnixTime(this DateTime datetime, TimeSpan? ts = null)
        {
            if (!ts.HasValue) ts = new TimeSpan();
            DateTime dt = DateTime.UtcNow;
            dt = dt.Add(ts.Value);
            var timeSpan = (dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (ulong)timeSpan.TotalSeconds * 1000;
        }

        /// <summary>
        /// custom extension 
        /// </summary>
        /// <param name="log">
        /// </param>
        /// <param name="message">
        /// </param>
        public static void Dbg(this ILog log, string message)
        {
#if DEBUG
            log.Debug(message);
#endif
        }
    }
}