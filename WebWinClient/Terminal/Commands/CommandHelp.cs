using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Commands
{
    public class CommandHelp : Interfaces.ITerminalCommand
    {
        public string[] Command => new string[2] { "?", "help" };

        public bool ShowOnOpen => false;

        public string HelpCommandsText => "help | ?";

        public string HelpInfoText => "Shows the Help Informations";

        public IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters)
        {
            var retList = new List<TerminalLine>
            {
                new TerminalLine
                {
                    IsSystem = true,
                    Text = Terminal.SYSTEMLINESPLIT,
                }, new TerminalLine
                {
                    IsSystem = true,
                    Text = "Terminal Help"
                }
            };

            List<TerminalLine> lines = new List<TerminalLine>();

            foreach (Interfaces.ITerminalHelp transformHelp in Terminal.TerminalTransformInputs)
            {
                lines.Add(new TerminalLine
                {
                    IsSystem = true,
                    Text = transformHelp.HelpCommandsText,
                    SubText = transformHelp.HelpInfoText
                });
            }

            foreach (Interfaces.ITerminalHelp commandHelp in Terminal.TerminalCommands)
            {
                lines.Add(new TerminalLine
                {
                    IsSystem = true,
                    Text = commandHelp.HelpCommandsText,
                    SubText = commandHelp.HelpInfoText
                });
            }

            foreach (var item in lines.OrderBy(x => x.Text))
            {
                retList.Add(item);
            }

            retList.Add(new TerminalLine
            {
                IsSystem = true,
                Text = Terminal.SYSTEMLINESPLIT,
            });

            return retList;
        }
    }
}
