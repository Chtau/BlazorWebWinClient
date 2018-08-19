using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Interfaces
{
    public interface ITerminalCommandModule
    {
        IList<TerminalLine> ExecuteModule(string command, string[] parameters, ITerminalModule terminalModule);
    }
}
