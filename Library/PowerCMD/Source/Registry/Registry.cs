using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public class Registry
    {
        public List<Command> Entries = new List<Command>();


        public void Register(string Identifier, MethodInfo CommandLogic, bool OutputReturn = true)
        {
            Identifier.Replace(" ", String.Empty);

            for (int I = 0; I < Entries.Count; I++)
            {
                if (Entries[I].Identifier == Identifier)
                {
                    Output.Write($"Command `{Identifier}` already registered with the exact same identifier.", Output.Severity.Warning, this);
                }
            }

            Command NewCommand = new Command(Identifier, CommandLogic, OutputReturn);
            Entries.Add(NewCommand);
        }

        public void Remove(string Identifier)
        {
            Identifier.Replace(" ", String.Empty);

            for (int I = 0; I < Entries.Count; I++)
            {
                if (Entries[I].Identifier == Identifier){
                    Entries.RemoveAt(I);
                }
            }
        }


        public Command IsRegistered(string Identifier, bool LogCommandData = false)
        {
            for (int I = 0; I < Entries.Count; I++)
            {
                if (Entries[I].Identifier == Identifier)
                {
                    if (LogCommandData) {
                        Output.Write($"Command '{Identifier}' found with {Entries[I].LogicParameters.Length} required parameter{((Entries[I].LogicParameters.Length != 1) ? "s" : "")}.", Output.Severity.Information, this);
                    }
                    
                    return Entries[I];
                }
            }

            return null;
        }
    }
}
