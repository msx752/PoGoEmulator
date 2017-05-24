using Google.Protobuf;
using HttpMachine;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using PoGoPrivate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace PoGoPrivate
{
    public static class Extensions
    {
        public static string JoinLines(this Dictionary<string, string> headers)
        {
            return string.Join(Environment.NewLine, headers.Select(x => x.Key + ": " + x.Value).ToArray()) + Environment.NewLine;
        }

        public static MyHttpContext GetContext(this NetworkStream stream, CancellationToken ct)
        {
            try
            {
                var handler = new MyHttpContext();
                var parser = new HttpParser(handler);

                var buffer = new byte[Global.MaxRequestContentLength];

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
            catch (ObjectDisposedException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
#endif
            }
            catch (OperationCanceledException e)
            {
#if DEBUG
                Logger.Write(e.Message, LogLevel.TaskIssue);
#endif
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, LogLevel.Error);
            }
            return null;
        }

        public static T Proton<T>(this Connection cnnUser) where T : class
        {
            CodedInputStream codedStream = new CodedInputStream(cnnUser.HttpContext.body.First());
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
    }
}