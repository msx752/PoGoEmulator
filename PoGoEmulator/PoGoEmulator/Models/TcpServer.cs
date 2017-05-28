using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PoGoEmulator.Models
{
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
#if DEBUG
                    Logger.Write("waiting connection..", LogLevel.Debug);
#endif
                    var client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);

#pragma warning disable 4014
                    Task.Factory.StartNew(() =>
#pragma warning restore 4014
                  {
#if DEBUG
                      Logger.Write($"connected from {client.Client.RemoteEndPoint}", LogLevel.Debug);
#endif
                      new Connection(client).Answer();
                  }, _ct);

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
    }
}