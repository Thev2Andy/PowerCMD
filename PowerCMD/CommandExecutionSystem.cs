using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class CommandExecutionSystem
    {
        public static object ExecuteCommand(string Identifier, params object[] Parameters)
        {
            if (CommandRegistry.IsCommandRegistered(Identifier) != null)
            {
                try {
                    object CommandReturn = CommandRegistry.IsCommandRegistered(Identifier)?.Execute(Parameters);
                    return CommandReturn;
                }catch (Exception Ex) {
                    // Handle invalid input handling..
                    string ErrorMessage = Ex.Message;
                    ErrorMessage += ((Ex.Message == "Parameter count mismatch.") ? $" Expected {CommandRegistry.IsCommandRegistered(Identifier)?.LogicParameters.Length} parameter{((CommandRegistry.IsCommandRegistered(Identifier)?.LogicParameters.Length != 1) ? "s" : "")}." : "");
                    CommandOutput.Output(ErrorMessage, CommandOutputType.Error);
                    return null;
                }
            }else {
                CommandOutput.Output($"Command '{Identifier}' not found.", CommandOutputType.Error);
                return null;
            }
        }
    }
}
