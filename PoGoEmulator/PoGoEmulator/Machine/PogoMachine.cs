using Microsoft.EntityFrameworkCore;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using System;
using System.Threading;

namespace PoGoEmulator.Machine
{
    public class PogoMachine
    {
        public PogoMachine()
        {
            new Thread(Run).Start();
        }

        #region fields

        /// <summary>
        /// request server 
        /// </summary>
        public TcpServer Server { get; private set; }

        #endregion fields

        #region methods

        public void Run()
        {
            try
            {
                Server = new TcpServer();
                Server.StartServer(Global.Cfg.Ip, Global.Cfg.Port);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Stop()
        {
#if DEBUG

            Logger.Write("machine is stopped.", LogLevel.Debug);
#endif
            Server.Stop();
        }

        #endregion methods
    }
}