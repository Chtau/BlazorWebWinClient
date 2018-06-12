using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Interfaces
{
    public interface ITerminalHelp
    {
        string HelpCommandsText { get; }
        string HelpInfoText { get; }
    }
}
