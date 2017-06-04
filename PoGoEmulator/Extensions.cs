using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf;
using Microsoft.DotNet.InternalAbstractions;
using Microsoft.Extensions.DependencyModel;
using PoGoEmulator.Controllers;

namespace PoGoEmulator
{
    public static class Extensions
    {
        /// <summary>
        /// protobuf file deserialise on pure byte[] file , (becareful object must be a type of proto )
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="protobuf">
        /// </param>
        /// <returns>
        /// </returns>
        public static T ProtoSerializer<T>(this Byte[] protobuf) where T : class
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

        //        /// <summary>
        //        /// custom extension
        //        /// </summary>
        //        /// <param name="log">
        //        /// </param>
        //        /// <param name="message">
        //        /// </param>
        //        public static void Dbg(this ILog log, string message)
        //        {
        //#if DEBUG
        //            log.Debug(message);
        //#endif
        //        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static ulong ToUnixTime(this DateTime d, TimeSpan? ts = null)
        {
            if (!ts.HasValue)
                ts = new TimeSpan();
            DateTime dt = DateTime.Now;
            dt = dt.Add(ts.Value);
            var timeSpan = (dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (ulong)timeSpan.TotalSeconds * 1000;
        }

        public static Type FindTypeOfObject(string qualifiedTypeName)
        {
            var t = Type.GetType(qualifiedTypeName);
            if (t != null)
                return t;
            else
            {
                foreach (var asm in GetAssemblies())
                {
                    if (asm.FullName == qualifiedTypeName)
                        t = asm.GetType();
                    if (t != null)
                        return t;
                }
                return null;
            }
        }

        public static IEnumerable<AssemblyName> GetAssemblies()
        {
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            var assemblies = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId);
            return assemblies;
        }
    }
}