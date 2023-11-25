using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public struct Return
    {
        public Object FunctionResult { get; private set; }
        public Result Result { get; private set; }

        public Return(Result Result, Object FunctionResult = null) {
            this.FunctionResult = FunctionResult;
            this.Result = Result;
        }
    }
}
