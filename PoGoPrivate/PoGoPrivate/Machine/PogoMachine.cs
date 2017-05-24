using Microsoft.EntityFrameworkCore;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using PoGoPrivate.Models;
using System;
using PoGoPrivate.EntityFramework;

namespace PoGoPrivate.Machine
{
    public class PogoMachine
    {
        #region fields

        private TcpServer _slistener;

        private readonly DatabaseService _database;
        public TcpServer Server { get { return _slistener; } }
        public DatabaseService Database { get { return _database; } }

        #endregion fields

        public PogoMachine()
        {
            try
            {
                _database = new DatabaseService();
                var newConfig = _database.Configure().Builder.UseInMemoryDatabase();
                _database.UseConfiguration(newConfig);
                Logger.Write("Successfuly connected to database server.");
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, LogLevel.Error);
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
                Logger.Write(e.Message, LogLevel.Error);
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