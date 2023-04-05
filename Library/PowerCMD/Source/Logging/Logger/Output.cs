using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public static class Output
    {
        public static EventHandler<OutputArgs> OnOutput;

        public static void Write(string Message, OutputType MessageType = OutputType.Information, Object Sender = null)
        {
            OutputArgs OutputArgs = new OutputArgs()
            {
                OutputMessage = Message,
                OutputType = MessageType
            };

            OnOutput?.Invoke(Sender, OutputArgs);
        }
    }
}
