using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public class Command
    {
        public readonly string Identifier;
        public readonly MethodInfo Logic;
        public readonly ParameterInfo[] LogicParameters;
        public readonly bool OutputReturn;

        public Command(string Identifier, MethodInfo Logic, bool OutputReturn) {
            this.Identifier = Identifier;
            this.Logic = Logic;
            this.LogicParameters = Logic.GetParameters();
            this.OutputReturn = OutputReturn;
        }

        public CommandReturn Execute(params object[] Parameters) {
            object CommandReturn = null;
            try {
                CommandReturn = Logic.Invoke(null, Parameters);
            }

            catch (Exception Ex) {
                if (Ex.InnerException != null) {
                    CommandOutput.Output("An exception occured in the called function..", CommandOutputType.Error);
                    CommandOutput.Output(Ex.Message, CommandOutputType.Error);
                    CommandOutput.Output(Ex.InnerException.Message, CommandOutputType.Error);
                    return new CommandReturn(CommandResult.Fail);
                }

                else {
                    // Handle invalid input handling in the method parameters..
                    string ErrorMessage = Ex.Message;
                    ErrorMessage += ((Ex.Message == "Parameter count mismatch.") ? $" Expected {CommandRegistry.IsCommandRegistered(Identifier)?.LogicParameters.Length} parameter{((CommandRegistry.IsCommandRegistered(Identifier)?.LogicParameters.Length != 1) ? "s" : "")}." : "");
                    CommandOutput.Output(ErrorMessage, CommandOutputType.Error);
                    return new CommandReturn(CommandResult.Fail);
                }
            }


            if (OutputReturn && CommandReturn != null) {
                CommandOutput.Output($"Command returned an object of type '{CommandReturn.GetType()}': '{CommandReturn.ToString()}'", CommandOutputType.Null);
            }

            return new CommandReturn(CommandResult.Success, CommandReturn);
        }
    }
}
