using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PoGoEmulator.Enums;
using PoGoEmulator.Logging;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace

namespace PoGoEmulator.Models
{
    public class UserConnection : RouteConnection
    {
        public ConnectionTick CnnTick { get; }

        public UserConnection(TcpClient client, CancellationTokenSource cancelTokenSource)
            : base(client, cancelTokenSource)
        {
            CnnTick = new ConnectionTick(cancelTokenSource.Token, this, true);
        }

        public override HttpStreamResult OnRequest()
        {
            try
            {
                if (IsRpcRequest)
                {
                    return base.OnRequest();
                }
                else
                {
                    throw new Exception("it is not rpcRequest");
                }
            }
            catch (Exception e)
            {
                Logger.Write(e);
                return Abort(RequestState.AbortedBySystem, e);
            }
        }

        public override HttpStreamResult Abort(RequestState state, Exception e = null)
        {
            CnnTick.Stop();
            return base.Abort(state, e);
        }
    }
}