using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWinClient.Terminal
{
    public static class BuildModule
    {
        public static IList<Interfaces.ITerminalModule> FromRawData(byte[] rawData)
        {
            var assembly = System.Reflection.Assembly.Load(rawData);
            IList<Interfaces.ITerminalModule> retVal = new List<Interfaces.ITerminalModule>();
            foreach (var exType in assembly.GetExportedTypes())
            {
                var terminalModule = new Modules.ModuleAssembly
                {
                    Namespace = exType.FullName,
                    Assembly = assembly,
                    MethodsNames = new List<Interfaces.MethodInfo>(),
                    RawData = rawData
                };

                foreach (var method in exType.GetMethods())
                {
                    var methodInfo = new Interfaces.MethodInfo
                    {
                        Name = method.Name
                    };

                    foreach (var inputParam in method.GetParameters())
                    {
                        if (!string.IsNullOrWhiteSpace(methodInfo.InputParameters))
                            methodInfo.InputParameters += " ,";
                        methodInfo.InputParameters += $"Name:{inputParam.Name} Type:{inputParam.ParameterType}";
                    }

                    terminalModule.MethodsNames.Add(methodInfo);
                }

                retVal.Add(terminalModule);
            }
            return retVal;
        }
    }
}
