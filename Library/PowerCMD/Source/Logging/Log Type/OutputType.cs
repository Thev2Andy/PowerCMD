using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public enum OutputType
    {
        Verbose,
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Network,
        Fatal,
        NA
    }
}
