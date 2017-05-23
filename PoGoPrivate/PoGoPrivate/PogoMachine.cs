using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BetterEntityFramework;
using BetterEntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using PoGoPrivate.Enums;
using PoGoPrivate.Logging;
using PoGoPrivate.Requests;

namespace PoGoPrivate
{
    public class PogoMachine
    {
        private TcpServer slistener;
        public TcpServer Server { get { return slistener; } }

        private readonly DataService database;
        public DataService Database { get { return database; } }

        public PogoMachine()
        {
            Logger.AddLogger(new ConsoleLogger(LogLevel.Info));
#if DEBUG
            Logger.Write("ON", LogLevel.Debug);
#endif
            database = new DataService();
            var newConfig = database.Configure().Builder.UseInMemoryDatabase();
            database.UseConfiguration(newConfig);
            Logger.Write("Successfuly connected to database server.");
        }

        public void Run()
        {
            slistener = new TcpServer();
            slistener.StartServer(Global.ip, Global.port);
            Logger.Write($"Listening {Global.ip}:{Global.port}");
        }

        public void Stop()
        {
            slistener.Stop();
        }
    }
}