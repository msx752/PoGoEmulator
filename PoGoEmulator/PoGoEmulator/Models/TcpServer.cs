using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PoGoEmulator.Models
{
    /// <summary>
    /// Create new instance of TcpServer. 
    /// </summary>
    public class TcpServer
    {
        private CancellationToken _ct;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private TcpListener _listener;
        private bool listening = false;

        /// <summary>
        /// non blocking listener 
        /// </summary>
        /// <param name="ip">
        /// </param>
        /// <param name="port">
        /// </param>
        public async void StartServer(IPAddress ip, int port)
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

        /// <summary>
        /// Stops receiving incoming requests. 
        /// </summary>
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

        /// <summary>
        /// async handling 
        /// </summary>
        /// <param name="client">
        /// </param>
        private void HandleClient(Connection client)
        {
            client.Execute();
        }
    }
}