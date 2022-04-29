using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public class OutputArgs : EventArgs
    {
        public string OutputMessage;
        public OutputType OutputType;
    }
}
