using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;

namespace PoGoPrivate
{
    public class TcpServer
    {
        #region Public.

        // Create new instance of TcpServer.
        public TcpServer()
        {
        }

        public async void StartServer(IPAddress ip, int port) //non blocking listener
        {
            try
            {
                listening = true;
                _listener = new TcpListener(ip, port);
                _listener.Start(1000);

                _ct = _cts.Token;
                while (listening)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                    await Task.Factory.StartNew(async () => { HandleClient(new Connection(client)); }, _ct);
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

                    //quit shutdown//client.Client.Close();//client.Client.Dispose();
                }
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, LogLevel.Error);
                throw e;
            }
        }

        private void HandleClient(Connection client)
        {
            client.Execute();
        }

        // Stops receiving incoming requests.
        public void Stop()
        {
            listening = false;
            // If listening has been cancelled, simply go out from method.
            if (_ct.IsCancellationRequested)
            {
                return;
            }

            // Cancels listening.
            _cts.Cancel();

            // Waits a little, to guarantee that all operation receive information about cancellation.
            Thread.Sleep(100);
            _listener.Stop();
        }

        #endregion Public.

        #region Fields.

        private bool listening = false;
        private CancellationToken _ct;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private TcpListener _listener;

        #endregion Fields.
    }
}