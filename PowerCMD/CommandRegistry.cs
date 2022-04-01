using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class CommandRegistry
    {
        public static List<Command> RegisteredCommands = new List<Command>();

        public static void RegisterCommand(string CommandIdentifier, MethodInfo CommandLogic, bool OutputReturn = true)
        {
            CommandIdentifier.Replace(" ", "_");
            for (int i = 0; i < RegisteredCommands.Count; i++)
            {
                if (RegisteredCommands[i].Identifier == CommandIdentifier)
                {
                    CommandOutput.Output("Command already registered with the exact same identifier.", CommandOutputType.Warning);
                }
            }

            Command NewCommand = new Command(CommandIdentifier, CommandLogic, OutputReturn);
            RegisteredCommands.Add(NewCommand);
        }

        public static void UnregisterCommand(string CommandIdentifier)
        {
            CommandIdentifier.Replace(" ", "_");
            for (int i = 0; i < RegisteredCommands.Count; i++)
            {
                if (RegisteredCommands[i].Identifier == CommandIdentifier){
                    RegisteredCommands.RemoveAt(i);
                }
            }
        }

        public static Command IsCommandRegistered(string CommandIdentifier, bool LogCommandFound = false)
        {
            for (int i = 0; i < RegisteredCommands.Count; i++)
            {
            if (RegisteredCommands[i].Identifier == CommandIdentifier)
            {
                if (LogCommandFound) CommandOutput.Output($"Command '{CommandIdentifier}' found with {RegisteredCommands[i].LogicParameters.Length} required parameter{((RegisteredCommands[i].LogicParameters.Length != 1) ? "s" : "")}.");
                    return RegisteredCommands[i];
                }
            }

            return null;
        }
    }
}
