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

        public object Execute(params object[] Parameters) {
            object CommandReturn = Logic.Invoke(null, Parameters);
            if (OutputReturn && CommandReturn != null) {
                CommandOutput.Output($"Command returned an object of type '{CommandReturn.GetType()}': '{CommandReturn.ToString()}'", CommandOutputType.Null);
            }

            return CommandReturn;
        }
    }
}
