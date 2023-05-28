using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public class ExecutionSystem
    {
        public Registry Registry { get; private set; }

        public Return Execute(string Identifier, params object[] Parameters)
        {
            if (Registry.IsRegistered(Identifier) != null) {
                Return CommandReturn = (Return)(Registry.IsRegistered(Identifier)?.Execute(Parameters));
                return CommandReturn;

            }
        
            else {
                Output.Write($"Command '{Identifier}' not found.", Output.Severity.Error, this);
                return new Return(Result.Fail);
            }
        }

        public ExecutionSystem(Registry Registry) {
            this.Registry = Registry;
        }
    }
}
