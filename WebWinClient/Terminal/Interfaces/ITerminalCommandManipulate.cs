using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Interfaces
{
    public interface ITerminalCommandManipulate
    {
        void ManipulateLines(ref IList<TerminalLine> lines);
    }
}
