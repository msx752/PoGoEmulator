using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using System.Threading;
using HttpMachine;

namespace PoGoPrivate
{
    public static class Extensions
    {
        public static string JoinLines(this Dictionary<string, string> headers)
        {
            return string.Join(Environment.NewLine, headers.Select(x => x.Key + ": " + x.Value).ToArray()) + Environment.NewLine;
        }

        private static string ReadAllLinesWithPeek(NetworkStream stream)
        {
            StreamReader sr = new StreamReader(stream);
            string input = "";
            while (sr.Peek() >= 0)
            {
                input += (char)sr.Read();
            }
            return input;
        }

        public static MyHttpParserDelegate GetHeaders(this NetworkStream stream, CancellationToken ct)
        {
            try
            {
                var handler = new MyHttpParserDelegate();
                var parser = new HttpParser(handler);

                var buffer = new byte[Global.maxRequestContentLength];

                int bytesRead = stream.Read(buffer, 0, buffer.Length);
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
    }
}