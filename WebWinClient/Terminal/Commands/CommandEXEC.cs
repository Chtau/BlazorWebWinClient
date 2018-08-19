using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWinClient.Terminal.Interfaces;

namespace WebWinClient.Terminal.Commands
{
    public class CommandEXEC : Interfaces.ITerminalCommand, Interfaces.ITerminalCommandModule
    {
        public string[] Command => new string[1] { "exec" };

        public bool ShowOnOpen => false;

        public string HelpCommandsText => "exec";

        public string HelpInfoText => "Executes a Method function";

        public IList<TerminalLine> ExecuteCommand(Terminal instance, string command, string[] parameters)
        {
            return null;
        }

        public IList<TerminalLine> ExecuteModule(string command, string[] parameters, ITerminalModule terminalModule)
        {
            List<TerminalLine> lines = new List<TerminalLine>();

            if (parameters == null || parameters.Length == 0)
            {
                lines.Add(new TerminalLine
                {
                    IsSystem = true,
                    Text = "No Parameters for \"exec\" was given"
                });
                lines.Add(new TerminalLine
                {
                    IsSystem = true,
                    Text = "Command usage \"exec String()\"."
                });
                lines.Add(new TerminalLine
                {
                    IsSystem = true,
                    Text = "Check with \"dir\" the required Parameters for the Methode."
                });
            } else
            {
                try
                {
                    if (terminalModule is Modules.ModuleAssembly modAssembly)
                    {
                        string fullParams = "";
                        foreach (var item in parameters)
                        {
                            fullParams += item;
                        }
                        if (fullParams.Contains("(") && fullParams.Contains(")"))
                        {
                            int paramStartIndex = fullParams.IndexOf('(');
                            int paramEndIndex = fullParams.LastIndexOf(')');

                            string methodName = fullParams.Substring(0, paramStartIndex);
                            string[] paramStringTmp = fullParams.Trim()?.Substring(paramStartIndex + 1)?.Replace("(", "")?.Replace(")", "")?.Trim()?.Split(',');
                            List<string> paramString = new List<string>();
                            foreach (var item in paramStringTmp)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                    paramString.Add(item);
                            }

                            var exType = modAssembly.Assembly.GetExportedTypes().FirstOrDefault(x => x.FullName.ToLower() == modAssembly.Namespace.ToLower());
                            System.Reflection.MethodInfo method = null;

                            method = exType.GetMethods().FirstOrDefault(x => x.Name.ToLower() == methodName.ToLower() && x.GetParameters().Count() == paramString.Count());

                            if (method != null)
                            {
                                List<Type> paramTypes = new List<Type>();
                                foreach (var inputParam in method.GetParameters())
                                {
                                    paramTypes.Add(inputParam.ParameterType);
                                }

                                List<object> paramValues = null;
                                if (paramString != null && paramString.Count > 0)
                                {
                                    paramValues = new List<object>();
                                    for (int i = 0; i < paramString.Count; i++)
                                    {
                                        if (i >= paramTypes.Count)
                                        {
                                            Type t = paramTypes[paramTypes.Count - 1];
                                            object val = Convert.ChangeType(paramString[i].Trim(), t);
                                            paramValues.Add(val);
                                        }
                                        else
                                        {
                                            Type t = paramTypes[i];
                                            object val = Convert.ChangeType(paramString[i].Trim(), t);
                                            paramValues.Add(val);
                                        }
                                    }
                                }

                                var o = Activator.CreateInstance(exType);
                                var result = method.Invoke(o, paramValues?.ToArray());
                                if (result == null)
                                {
                                    lines.Add(new TerminalLine
                                    {
                                        IsSystem = true,
                                        Text = method.Name + " executed."
                                    });
                                }
                                else
                                {
                                    lines.Add(new TerminalLine
                                    {
                                        IsSystem = true,
                                        Text = method.Name + " executed. Result: " + result.ToString()
                                    });
                                }
                            } else
                            {
                                lines.Add(new TerminalLine
                                {
                                    IsSystem = true,
                                    Text = "Methode not found",
                                    Type = TerminalLine.LineType.Warning
                                });
                            }
                        } else
                        {
                            lines.Add(new TerminalLine
                            {
                                IsSystem = true,
                                Text = "Methode Syntax is invalid",
                                Type = TerminalLine.LineType.Warning
                            });
                        }
                    }
                } catch (Exception ex)
                {
                    lines.Add(new TerminalLine
                    {
                        IsSystem = true,
                        Text = ex.Message,
                        Type = TerminalLine.LineType.Error
                    });
                }
            }
            return lines;
        }
    }
}
