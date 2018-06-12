using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.TransformInputs
{
    public class TransformDatetime : Interfaces.ITerminalTransformInput
    {
        public string[] Pattern { get => new string[2] { @"@datetime", @"@now" }; }
        public object Replacement { get => DateTime.Now; }
        public string HelpCommandsText { get => "@datetime | @now"; }
        public string HelpInfoText { get => "Will be replaced with the current Datetime"; }
    }
}
