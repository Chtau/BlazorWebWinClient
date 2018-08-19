using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebWinClient.Terminal
{
    public class Terminal
    {
        public const string SYSTEMLINESPLIT = "----------------------------------------------------------------------------------------------------";
        public const string STORAGE_MODULE_KEY = "assemblyModulesRawData";

        public static IList<Interfaces.ITerminalTransformInput> TerminalTransformInputs = new List<Interfaces.ITerminalTransformInput>();
        public static IList<Interfaces.ITerminalCommand> TerminalCommands = new List<Interfaces.ITerminalCommand>();
        public static IList<Interfaces.ITerminalModule> TerminalModules = new List<Interfaces.ITerminalModule>();

        public static void AddTerminalModules(IList<Interfaces.ITerminalModule> modules)
        {
            foreach (var terminalModule in modules)
            {
                if (!TerminalModules.Any(x => x.Namespace == terminalModule.Namespace))
                {
                    TerminalModules.Add(terminalModule);
                }
            }
        }

        public static void StorageSaveModules()
        {
            List<byte[]> assMods = new List<byte[]>();
            foreach (var item in TerminalModules)
            {
                if (item is Modules.ModuleAssembly assemblyMod)
                {
                    assMods.Add(assemblyMod.RawData);
                }
            }

            Storage.WriteStorage<List<byte[]>>(STORAGE_MODULE_KEY, assMods);

        }

        public static void StorageLoadModules()
        {
            List<byte[]> assMods = Storage.ReadStorage<List<byte[]>>(STORAGE_MODULE_KEY);
            foreach (var item in assMods)
            {
                AddTerminalModules(BuildModule.FromRawData(item));
            }
        }

        public static void StorageClearModules()
        {
            Storage.RemoveStorage(STORAGE_MODULE_KEY);
        }

        public static void AddDefaults()
        {
            TerminalTransformInputs.Add(new TransformInputs.TransformDatetime());

            TerminalCommands.Add(new Commands.CommandInfo());
            TerminalCommands.Add(new Commands.CommandHelp());
            TerminalCommands.Add(new Commands.CommandCLS());
            TerminalCommands.Add(new Commands.CommandDIR());
            TerminalCommands.Add(new Commands.CommandCD());
            TerminalCommands.Add(new Commands.CommandEXEC());

            StorageLoadModules();
        }

        private int InputHistoryIndex = 0;
        private IList<string> InputHistory { get; set; }

        public IList<TerminalLine> TerminalLines { get; set; }
        
        public string ActiveNamespace = "";
        public string GetActiveDisplayNamespace()
        {
            return "/" + ActiveNamespace + "/";
        }

        public Terminal()
        {
            InputHistory = new List<string>();
            TerminalLines = new List<TerminalLine>
            {
                new TerminalLine
                {
                    Readonly = false
                }
            };
            foreach (Interfaces.ITerminalCommand cmd in TerminalCommands?.Where(x => x.ShowOnOpen))
            {
                var lines = cmd.ExecuteCommand(this, null, null);
                if (lines != null)
                {
                    foreach (var line in lines)
                    {
                        TerminalLines.Insert(TerminalLines.Count - 1, line);
                    }
                }
            }
        }

        public void AddLine(TerminalLine terminalLine)
        {
            string commandOnly = terminalLine.Text.TrimStart();
            InputHistory.Add(commandOnly);
            OnTransformInput(ref terminalLine);

            var line = terminalLine;
            if (!line.IsSystem)
            {
                if (!string.IsNullOrWhiteSpace(ActiveNamespace))
                {
                    line.Text = GetActiveDisplayNamespace() + line.Text;
                }
            }

            TerminalLines.Insert(TerminalLines.Count - 1, line);
            OnCheckForKnownCommands(commandOnly);
        }

        public string GetInputHistory(int addIndex)
        {
            int newIndex = InputHistoryIndex + addIndex;
            if (newIndex < 1)
                newIndex = (InputHistory.Count - 1);
            if (newIndex > (InputHistory.Count - 1))
                newIndex = 1;
            InputHistoryIndex = newIndex;
            return InputHistory[InputHistoryIndex];
        }

        public void ResetInputHistory()
        {
            this.InputHistoryIndex = 1;
        }

        private void OnTransformInput(ref TerminalLine terminalLine)
        {
            if (terminalLine.IsSystem)
                return;

            foreach (Interfaces.ITerminalTransformInput transformInput in TerminalTransformInputs)
            {
                foreach (string pattern in transformInput.Pattern)
                {
                    terminalLine.Text = OnReplace(terminalLine.Text, pattern, transformInput.Replacement.ToString());
                }
            }
        }

        private string OnReplace(string input, string pattern, string replacement)
        {
            Regex rgx = new Regex(pattern);
            return rgx.Replace(input, replacement);
        }

        private void OnCheckForKnownCommands(string command)
        {
            command = command.TrimStart();
            if (string.IsNullOrWhiteSpace(command))
                return;
            string mainCmd = command;
            string[] cmdExtension = null;
            if (mainCmd.Contains(" "))
            {
                string[] cmds = mainCmd.Split(' ');
                mainCmd = cmds[0];
                string[] cmdExtensionTmp = cmds.Skip(1).ToArray();
                if (cmdExtensionTmp != null && cmdExtensionTmp.Length > 0)
                {
                    List<string> tmp = new List<string>();
                    foreach (var item in cmdExtensionTmp)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                            tmp.Add(item);
                    }
                    cmdExtension = tmp.ToArray();
                }
            }
            mainCmd = mainCmd.ToLower();

            foreach (Interfaces.ITerminalCommand cmd in TerminalCommands)
            {
                foreach (string cmdPattern in cmd.Command)
                {
                    if (cmdPattern.ToLower() == mainCmd)
                    {
                        if (cmd is Interfaces.ITerminalCommandManipulate mani)
                        {
                            IList<TerminalLine> currentLines = TerminalLines;
                            mani.ManipulateLines(ref currentLines);
                            TerminalLines = currentLines;
                        }
                        if (cmd is Interfaces.ITerminalCommandModule mod)
                        {
                            if (TerminalModules.Any(x => x.Namespace.ToLower() == ActiveNamespace.ToLower()))
                            {
                                var module = TerminalModules.First(x => x.Namespace.ToLower() == ActiveNamespace.ToLower());
                                var linesModules = mod.ExecuteModule(mainCmd, cmdExtension, module);
                                if (linesModules != null)
                                {
                                    foreach (var line in linesModules)
                                    {
                                        TerminalLines.Insert(TerminalLines.Count - 1, line);
                                    }
                                }
                            }
                        }
                        var lines = cmd.ExecuteCommand(this, mainCmd, cmdExtension);
                        if (lines != null)
                        {
                            foreach (var line in lines)
                            {
                                TerminalLines.Insert(TerminalLines.Count - 1, line);
                            }
                        }
                        return;
                    }
                }
            }
        }
    }
}
