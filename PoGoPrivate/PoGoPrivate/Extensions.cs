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

namespace PoGoPrivate
{
    public static class Extensions
    {
        public static string JoinLines(this Dictionary<string, string> headers)
        {
            return string.Join(Environment.NewLine, headers.Select(x => x.Key + ": " + x.Value).ToArray()) + Environment.NewLine;
        }

        public static Dictionary<string, string> GetHeaders(this NetworkStream stream, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                var headers = new Dictionary<string, string>();
                byte[] data = new byte[Global.maxRequestContentLength];
                int numBytesRead = stream.Read(data, 0, data.Length);
                if (numBytesRead > 0)
                {
                    string[] str = Encoding.Default.GetString(data, 0, numBytesRead).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    data = null;
                    bool reachedToData = false;
                    foreach (var headerLine in str)
                    {
                        ct.ThrowIfCancellationRequested();
                        if (reachedToData)
                        {
                            headers["Protobuf"] += Environment.NewLine + headerLine;
                            continue;
                        }
                        var headerParts = headerLine.Split(' ');
                        if (headerParts.Length > 1)
                            headers[headerParts[0].TrimEnd(':')] = headerLine.Substring(headerParts[0].Length + 1);
                        else
                        {
                            headers["Protobuf"] = "";
                            reachedToData = true;
                        }
                    }
                    str = null;
                }
                return headers;
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