using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using PoGoPrivate.Requests;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PoGoPrivate.Models
{
    public sealed class Connection : IDisposable
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private MyHttpContext _httpContext;
        private TcpClient client;
        private bool completed;
        private bool IsDisposed = false;
        private Stopwatch stopwatch;
        private NetworkStream stream;
        private Timer tmr;

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
            _httpContext = Stream.GetContext(_cts.Token);
        }

        public bool Finished
        {
            get
            {
                if (stopwatch?.ElapsedMilliseconds > Global.Cfg.RequestTimeout.TotalMilliseconds || completed)
                    return true;
                else
                    return false;
            }
        }

        public MyHttpContext HttpContext { get { return _httpContext; } }
        public NetworkStream Stream { get { return stream; } }

        public void Abort(bool isUserCanceled = false)
        {
            //You can write out an abort message to the client if you like. (Stream.Write()....)
            this.Dispose(isUserCanceled);
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
            _httpContext = null;

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

        public void Execute()
        {
            try
            {
                Logger.Write(HttpContext.headers.JoinLines(), LogLevel.Response);
                RequestHandler.Parse(this, _cts.Token);
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

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_cts.Token.IsCancellationRequested)
                return;

            if (this.client.Client.Poll(1, SelectMode.SelectRead) && this.client.Client.Available == 0)//detect the custom aborting
                this.Abort(true);
            else if (Finished)
                this.Abort();
        }
    }
}