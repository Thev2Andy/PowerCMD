using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class Registry
    {
        public static List<Command> Entries = new List<Command>();

        public static void Register(string CommandIdentifier, MethodInfo CommandLogic, bool OutputReturn = true)
        {
            // Replace whitespaces, to make commands easier to work with.
            CommandIdentifier.Replace(" ", String.Empty);

            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Identifier == CommandIdentifier)
                {
                    Output.Write("Command already registered with the exact same identifier.", OutputType.Warning);
                }
            }

            Command NewCommand = new Command(CommandIdentifier, CommandLogic, OutputReturn);
            Entries.Add(NewCommand);
        }

        public static void Remove(string Identifier)
        {
            // Replace whitespaces, to make commands easier to work with.
            Identifier.Replace(" ", String.Empty);

            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Identifier == Identifier){
                    Entries.RemoveAt(i);
                }
            }
        }

        public static Command IsRegistered(string Identifier, bool LogCommandFound = false)
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Identifier == Identifier)
                {
                    if (LogCommandFound) {
                        Output.Write($"Command '{Identifier}' found with {Entries[i].LogicParameters.Length} required parameter{((Entries[i].LogicParameters.Length != 1) ? "s" : "")}.");
                    }
                    
                    return Entries[i];
                }
            }

            return null;
        }
    }
}
