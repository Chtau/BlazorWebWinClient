using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Interfaces
{
    public interface ITerminalModule
    {
        string Namespace { get; set; }
        IList<MethodInfo> MethodsNames { get; set; }
    }

    public class MethodInfo
    {
        public string Name { get; set; }
        public string InputParameters { get; set; }
        public bool HasOutput { get; set; }

        public MethodInfo()
        {
            Name = "";
            InputParameters = "";
            HasOutput = false;
        }
    }
}
