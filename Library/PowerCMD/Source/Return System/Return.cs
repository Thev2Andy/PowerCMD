using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public struct Return
    {
        public Result Result { get; private set; }
        public Object ReturnObject { get; private set; }

        public Return(Result Result, Object ReturnObject = null) {
            this.ReturnObject = ReturnObject;
            this.Result = Result;
        }
    }
}
