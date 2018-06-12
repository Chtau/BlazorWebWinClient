using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Interfaces
{
    public interface ITerminalCommand : ITerminalHelp
    {
        string[] Command { get; }
        bool ShowOnOpen { get; }

        IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters);
    }
}
