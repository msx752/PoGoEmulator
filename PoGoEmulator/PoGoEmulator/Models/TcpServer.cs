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
        public void StartServer(IPAddress ip, int port)
        {
            try
            {
                listening = true;
                _listener = new TcpListener(ip, port);
                _listener.Start(1000);
                Logger.Write($"Listening {Global.Cfg.Ip}:{Global.Cfg.Port}", LogLevel.Success);
                Logger.Write("Server is running...", LogLevel.Success);

                _ct = _cts.Token;
                while (listening)
                {
                    var client = _listener.AcceptTcpClient();
                    new Thread(Queue).Start(new Connection(client));
                    //quit shutdown//client.Client.Close();//client.Client.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Queue(object state)
        {
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