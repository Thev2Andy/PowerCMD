using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class ExecutionSystem
    {
        public static Return Execute(string Identifier, params object[] Parameters)
        {
            if (Registry.IsRegistered(Identifier) != null) {
                Return CommandReturn = (Return)(Registry.IsRegistered(Identifier)?.Execute(Parameters));
                return CommandReturn;

            }
        
            else {
                Output.Write($"Command '{Identifier}' not found.", OutputType.Error);
                return new Return(Result.Fail);
            }
        }
    }
}
