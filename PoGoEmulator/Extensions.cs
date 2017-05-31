using Google.Protobuf;
using HttpMachine;
using PoGoEmulator.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator
{
    public static class Extensions
    {
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        public static T[] ToArray<T>(this ArraySegment<T> arraySegment)
        {
            T[] array = new T[arraySegment.Count];
            Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
            return array;
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

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static Type FindTypeOfObject(string qualifiedTypeName)
        {
            var t = Type.GetType(qualifiedTypeName);
            if (t != null)
                return t;
            else
            {
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = asm.GetType(qualifiedTypeName);
                    if (t != null)
                        return t;
                }
                return null;
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
        public static T ProtoCaster<T>(this Byte[] protobuf) where T : class
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