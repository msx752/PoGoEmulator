using Microsoft.EntityFrameworkCore;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;
using PoGoEmulator.Models;
using System;

namespace PoGoEmulator.Machine
{
    public class PogoMachine
    {
        #region fields

        //private readonly DatabaseService _database;
        private TcpServer _slistener;

        //public DatabaseService Database { get { return _database; } }
        public TcpServer Server { get { return _slistener; } }

        #endregion fields

        public PogoMachine()
        {
            try
            {
                //_database = new DatabaseService();
                //var newConfig = _database.Configure().Builder.UseInMemoryDatabase();
                //_database.UseConfiguration(newConfig);
                Logger.Write("Successfuly connected to database server.");
            }
            catch (Exception e)
            {
                Logger.Write(e);
            }
        }

        #region methods

        public void Run()
        {
            try
            {
                _slistener = new TcpServer();
                _slistener.StartServer(Global.Cfg.Ip, Global.Cfg.Port);
                Logger.Write($"Listening {Global.Cfg.Ip}:{Global.Cfg.Port}");
            }
            catch (Exception e)
            {
                Logger.Write(e);
            }
        }

        public void Stop()
        {
#if DEBUG

            Logger.Write("machine is stopped.", LogLevel.Debug);
#endif
            _slistener.Stop();
        }

        #endregion methods
    }
}