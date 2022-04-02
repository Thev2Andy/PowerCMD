using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class CommandExecutionSystem
    {
        public static CommandReturn ExecuteCommand(string Identifier, params object[] Parameters)
        {
            if (CommandRegistry.IsCommandRegistered(Identifier) != null) {
                CommandReturn CommandReturn = (CommandReturn)(CommandRegistry.IsCommandRegistered(Identifier)?.Execute(Parameters));
                return CommandReturn;

            }
        
            else {
                CommandOutput.Output($"Command '{Identifier}' not found.", CommandOutputType.Error);
                return new CommandReturn(CommandResult.Fail);
            }
        }
    }
}
