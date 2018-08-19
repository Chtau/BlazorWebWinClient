using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Commands
{
    public class CommandCLS : Interfaces.ITerminalCommand, Interfaces.ITerminalCommandManipulate
    {
        public string[] Command => new string[1] { "cls" };

        public bool ShowOnOpen => false;

        public string HelpCommandsText => "cls";

        public string HelpInfoText => "Clears the lines on the Screen";

        public IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters)
        {
            return null;
        }

        public void ManipulateLines(ref IList<TerminalLine> lines)
        {
            lines.Clear();
            lines = new List<TerminalLine>
            {
                new TerminalLine
                {
                    Readonly = false
                }
            };
        }
    }
}
