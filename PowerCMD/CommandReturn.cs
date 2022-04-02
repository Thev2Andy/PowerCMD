using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public struct CommandReturn
    {
        public CommandResult Result { get; private set; }
        public object ReturnObject { get; private set; }

        public CommandReturn(CommandResult Result, object ReturnObject = null) {
            this.ReturnObject = ReturnObject;
            this.Result = Result;
        }
    }
}
