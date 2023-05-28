using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public class Command
    {
        public string Identifier { get; private set; }
        public MethodInfo Logic { get; private set; }
        public ParameterInfo[] LogicParameters { get; private set; }
        public bool OutputReturn { get; private set; }


        public Return Execute(params object[] Parameters) {
            object CommandReturn = null;
            try {
                CommandReturn = Logic.Invoke(null, Parameters);
            }

            catch (Exception Ex) {
                if (Ex.InnerException != null) {
                    Output.Write($"An exception of type `{Ex.InnerException.GetType()}` occured in the invoked function..", Output.Severity.Error, this);
                    Output.Write(Ex.Message, Output.Severity.Error, this);
                    Output.Write(Ex.InnerException.Message, Output.Severity.Error, this);
                    Output.Write($"Stacktrace:{Environment.NewLine}{Ex.InnerException.StackTrace}", Output.Severity.Generic, this);

                    return new Return(Result.Fail);
                }

                else {
                    // Handle invalid input handling in the method parameters..
                    string ErrorMessage = Ex.Message;
                    ErrorMessage += ((Ex.Message == "Parameter count mismatch.") ? $" Expected {LogicParameters.Length} parameter{((LogicParameters.Length != 1) ? "s" : "")}." : "");
                    Output.Write(ErrorMessage, Output.Severity.Error, this);
                    
                    return new Return(Result.Fail);
                }
            }


            if (OutputReturn && CommandReturn != null) {
                Output.Write($"Command returned an object of type '{CommandReturn.GetType()}': '{CommandReturn.ToString()}'", Output.Severity.Generic);
            }

            return new Return(Result.Success, CommandReturn);
        }




        public Command(string Identifier, MethodInfo Logic, bool OutputReturn) {
            this.Identifier = Identifier;
            this.Logic = Logic;
            this.OutputReturn = OutputReturn;

            this.LogicParameters = Logic.GetParameters();
        }
    }
}
