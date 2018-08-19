using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal.Modules
{
    public class ModuleAssembly : Interfaces.ITerminalModule
    {
        public string Namespace { get; set; }
        public IList<Interfaces.MethodInfo> MethodsNames { get; set; }
        public System.Reflection.Assembly Assembly { get; set; }
        public byte[] RawData { get; set; }
    }
}
