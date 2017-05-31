using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoGoEmulator.Models
{
    public class FuncConnection : BaseConnection
    {
        public FuncConnection(TcpClient client, CancellationTokenSource cancelToken) : base(client, cancelToken)
        {
        }

        public void OnReuest()
        {
        }
    }
}