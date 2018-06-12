using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Interfaces
{
    public interface ITerminalTransformInput : ITerminalHelp
    {
        string[] Pattern { get; }
        object Replacement { get; }
    }
}
