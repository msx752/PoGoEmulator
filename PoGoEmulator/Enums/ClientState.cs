using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoGoEmulator.Enums
{
    public enum RequestState : int
    {
        Completed,
        Timeout,
        CanceledByUser,
        AbortedBySystem
    }
}