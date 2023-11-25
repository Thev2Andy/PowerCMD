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
        public string Identifier { get; private set; }
        public MethodInfo Logic { get; private set; }
        public ParameterInfo[] LogicParameters { get; private set; }
        public bool OutputReturn { get; private set; }


        public Return Execute(params Object[] Parameters)
        {
            Object CommandReturn = null;
            try {
                CommandReturn = Logic.Invoke(null, Parameters);
            }

            catch (Exception Exception)
            {
                if (Exception.InnerException != null) {
                    Output.Write(Exception.InnerException.Message, Output.Severity.Error, this);
                    return new Return(Result.Fail);
                }

                else
                {
                    // Handle invalid input handling in the method parameters..
                    string ErrorMessage = Exception.Message;
                    ErrorMessage += ((Exception.Message == "Parameter count mismatch.") ? $" Expected {LogicParameters.Length} parameter{((LogicParameters.Length != 1) ? "s" : "")}." : "");
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
