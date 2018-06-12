using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Commands
{
    public class CommandDIR : Interfaces.ITerminalCommand
    {
        public string[] Command => new string[1] { "dir" };

        public bool ShowOnOpen => false;

        public string HelpCommandsText => "dir";

        public string HelpInfoText => "List of all loaded Modules in the current Directory";

        public IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters)
        {
            List<TerminalLine> lines = new List<TerminalLine>();

            if (Terminal.TerminalModules == null || Terminal.TerminalModules.Count == 0)
            {
                lines.Add(new TerminalLine
                {
                    IsSystem = true,
                    Text = "No Modules found",
                });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(instance.ActiveNamespace))
                {
                    foreach (Interfaces.ITerminalModule module in Terminal.TerminalModules.Where(x => x.Namespace.ToLower() == instance.ActiveNamespace.ToLower()))
                    {
                        foreach (Interfaces.MethodInfo methode in module.MethodsNames)
                        {
                            lines.Add(new TerminalLine
                            {
                                IsSystem = true,
                                Text = methode.Name,
                                SubText = methode.InputParameters
                            });
                        }
                    }
                }
                else
                {
                    foreach (Interfaces.ITerminalModule module in Terminal.TerminalModules)
                    {
                        lines.Add(new TerminalLine
                        {
                            IsSystem = true,
                            Text = module.Namespace
                        });
                    }
                }
            }

            return lines;
        }
    }
}
