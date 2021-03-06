using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public class Registry
    {
        public List<Command> Entries = new List<Command>();

        public void Register(string Identifier, MethodInfo CommandLogic, bool OutputReturn = true)
        {
            // Replace whitespaces, to make commands easier to work with.
            Identifier.Replace(" ", String.Empty);

            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Identifier == Identifier)
                {
                    Output.Write($"Command `{Identifier}` already registered with the exact same identifier.", OutputType.Warning, this);
                }
            }

            Command NewCommand = new Command(Identifier, CommandLogic, OutputReturn);
            Entries.Add(NewCommand);
        }

        public void Remove(string Identifier)
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

        public Command IsRegistered(string Identifier, bool LogCommandData = false)
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Identifier == Identifier)
                {
                    if (LogCommandData) {
                        Output.Write($"Command '{Identifier}' found with {Entries[i].LogicParameters.Length} required parameter{((Entries[i].LogicParameters.Length != 1) ? "s" : "")}.", OutputType.Info, this);
                    }
                    
                    return Entries[i];
                }
            }

            return null;
        }
    }
}
