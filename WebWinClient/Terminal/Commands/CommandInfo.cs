using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWinClient.Terminal.Interfaces;

namespace WebWinClient.Terminal.Commands
{
    public class CommandInfo : Interfaces.ITerminalCommand
    {
        public string[] Command => new string[1] { "info" };

        public string HelpCommandsText => "info";

        public string HelpInfoText => "Terminal Version";

        public bool ShowOnOpen => true;

        public IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            return new List<TerminalLine>
            {
                new TerminalLine
                {
                    IsSystem = true,
                    Text = Terminal.SYSTEMLINESPLIT,
                }, new TerminalLine
                {
                    IsSystem = true,
                    Text = "Terminal Information",
                }, new TerminalLine
                {
                    IsSystem = true,
                    Text = assembly.FullName
                }, new TerminalLine
                {
                    IsSystem = true,
                    Text = "Use \"help\" or \"?\" to show commands",
                }, new TerminalLine
                {
                    IsSystem = true,
                    Text = Terminal.SYSTEMLINESPLIT,
                }
            };
        }
    }
}
