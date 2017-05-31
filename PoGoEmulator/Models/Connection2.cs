using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models
{
    public class Connection2 : FuncConnection
    {
        public ConnectionTick CnnTick { get; }

        public Connection2(TcpClient client, CancellationTokenSource cancelTokenSource)
            : base(client, cancelTokenSource)
        {
            CnnTick = new ConnectionTick(cancelTokenSource.Token, this, true);
        }
    }
}