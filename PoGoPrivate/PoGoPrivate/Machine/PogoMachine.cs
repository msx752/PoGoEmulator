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
        private TcpServer slistener;
        public TcpServer Server { get { return slistener; } }

        private readonly DataService database;
        public DataService Database { get { return database; } }

        public PogoMachine()
        {
#if DEBUG
            Logger.Write("ON", LogLevel.Debug);
#endif
            try
            {
                database = new DataService();
                var newConfig = database.Configure().Builder.UseInMemoryDatabase();
                database.UseConfiguration(newConfig);
                Logger.Write("Successfuly connected to database server.");
            }
            catch (Exception e)
            {
                Logger.Write(e.Message, LogLevel.Error);
            }
        }

        public void Run()
        {
            try
            {
                slistener = new TcpServer();
                slistener.StartServer(Global.ip, Global.port);
                Logger.Write($"Listening {Global.ip}:{Global.port}");
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
            slistener.Stop();
        }
    }
}