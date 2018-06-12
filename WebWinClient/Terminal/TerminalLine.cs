using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal
{
    public class TerminalLine
    {
        public enum LineType
        {
            Normal = 0,
            Error = 1,
            Warning = 2
        }

        public string Text { get; set; }
        public string SubText { get; set; }
        public bool Readonly { get; set; }
        public bool IsSystem { get; set; }
        public DateTime DateTime { get; set; }
        public LineType Type { get; set; }

        public TerminalLine()
        {
            Text = "";
            SubText = "";
            Readonly = true;
            IsSystem = false;
            DateTime = DateTime.Now;
            Type = LineType.Normal;
        }
    }
}
