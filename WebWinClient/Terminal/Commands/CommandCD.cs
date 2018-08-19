using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Commands
{
    public class CommandCD : Interfaces.ITerminalCommand
    {
        public string[] Command => new string[2] { "cd", "cd.." };

        public bool ShowOnOpen => false;

        public string HelpCommandsText => "cd";

        public string HelpInfoText => "Use with a Directory Name to switch to it";

        public IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters)
        {
            List<TerminalLine> lines = new List<TerminalLine>();

            if (command == "cd..")
            {
                instance.ActiveNamespace = "";
            } else
            {
                if (parameters == null || parameters.Length == 0)
                {
                    lines.Add(new TerminalLine
                    {
                        IsSystem = true,
                        Text = "No Parameter for \"cd\" was given"
                    });
                    lines.Add(new TerminalLine
                    {
                        IsSystem = true,
                        Text = "Command usage \"cd DEMO_LIB_NET_47.demo\" or \"cd..\" to go up in the tree"
                    });
                } else
                {
                    string nameSpace = parameters[0];
                    if (Terminal.TerminalModules.Any(x => x.Namespace.ToLower() == nameSpace.ToLower()))
                    {
                        instance.ActiveNamespace = Terminal.TerminalModules.FirstOrDefault(x => x.Namespace.ToLower() == nameSpace.ToLower()).Namespace;
                    } else
                    {
                        lines.Add(new TerminalLine
                        {
                            IsSystem = true,
                            Text = "Invalid Namespace"
                        });
                    }
                }
            }
            return lines;
        }
    }
}
