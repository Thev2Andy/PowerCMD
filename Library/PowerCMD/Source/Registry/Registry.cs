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
                if (Entries[I].Identifier == Identifier) {
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
                if (Entries[I].Identifier == Identifier) {
                    Entries.RemoveAt(I);
                }
            }
        }


        public Command IsRegistered(string Identifier)
        {
            for (int I = 0; I < Entries.Count; I++)
            {
                if (Entries[I].Identifier == Identifier) {
                    return Entries[I];
                }
            }

            return null;
        }
    }
}
