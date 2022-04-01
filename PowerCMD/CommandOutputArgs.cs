using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public class CommandOutputArgs : EventArgs
    {
        public string OutputMessage;
        public CommandOutputType CommandOutputType;
    }
}
