using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using PoGoPrivate.Requests;
using Timer = System.Timers.Timer;

namespace PoGoPrivate
{
    public sealed class Connection : IDisposable
    {
        private bool IsDisposed = false;
        private TcpClient client;
        private Stopwatch stopwatch;
        private NetworkStream stream;
        private Dictionary<string, string> headers;
        private Timer tmr;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        public Dictionary<string, string> Headers { get { return headers; } }
        public NetworkStream Stream { get { return stream; } }

        public Connection(TcpClient client)
        {
            _cts.Token.ThrowIfCancellationRequested();
            this.client = client;
            stopwatch = new Stopwatch();
            tmr = new Timer(150);

            tmr.Elapsed += Tmr_Elapsed;
            stopwatch.Start();
            Task.Run(() => tmr.Start(), _cts.Token);
            this.stream = this.client.GetStream();
            headers = Stream.GetHeaders(_cts.Token);
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_cts.Token.IsCancellationRequested)
                return;

            if (this.client.Client.Poll(1, SelectMode.SelectRead) && this.client.Client.Available == 0)//detect the custom aborting
                this.Abort(true);
            else if (Finished)
                this.Abort();
        }

        public void Execute()
        {
            try
            {
                Logger.Write(Headers.JoinLines(), LogLevel.Response);
                Request.Router(this, _cts.Token);
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
            this.Abort();
        }

        public void Abort(bool isUserCanceled = false)
        {
            //You can write out an abort message to the client if you like. (Stream.Write()....)
            this.Dispose(isUserCanceled);
        }

        private bool completed;

        public bool Finished
        {
            get
            {
                if (stopwatch?.ElapsedMilliseconds > Global.requestTimeout.TotalMilliseconds || completed)
                    return true;
                else
                    return false;
            }
        }

        public void Dispose(bool isUserCanceled)
        {
            if (IsDisposed) return;

#if DEBUG
            Logger.Write(
                isUserCanceled
                    ? $"session ended for {client.Client.RemoteEndPoint}, IsUserCanceled:{isUserCanceled}"
                    : $"session ended for {client.Client.RemoteEndPoint}, IsCompleted:{completed}, IsCanceled:{!completed}",
                LogLevel.Debug);
#endif
            completed = true;
            IsDisposed = true;

            tmr.Enabled = false;
            tmr.Dispose();
            _cts.Cancel(); //force stop
            Thread.Sleep(100);
            tmr = null;

            stopwatch = null;
            headers = null;

            client?.Close();
            ((IDisposable)client)?.Dispose();
            client = null;

            stream?.Close();
            stream?.Dispose();
            stream = null;
        }

        public void Dispose()
        {
            Dispose(false);
        }
    }
}