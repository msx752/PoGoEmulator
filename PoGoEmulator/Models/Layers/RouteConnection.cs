using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PoGoEmulator.Enums;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace

namespace PoGoEmulator.Models
{
    public class RouteConnection : FuncConnection
    {
        public ControllerType CtrlType { get; protected set; }

        public bool IsRpcRequest
        {
            get
            {
                return this.StreamContext.Url.Segments[2] == "rpc";
            }
        }

        public RouteConnection(TcpClient client, CancellationTokenSource cancelToken) : base(client, cancelToken)
        {
            switch (this.StreamContext.Url.Segments[1])
            {
                case "plfe/":
                    this.CtrlType = Enums.ControllerType.Plfe;
                    break;

                case "custom/":
                    this.CtrlType = Enums.ControllerType.Custom;
                    break;

                case "model/":
                    this.CtrlType = Enums.ControllerType.Model;
                    break;

                default:
                    this.CtrlType = Enums.ControllerType.None;
                    break;
            }
        }
    }
}