using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class CommandOutput
    {
        public static EventHandler<CommandOutputArgs> OnOutput;

        public static void Output(string Message = "", CommandOutputType MessageType = CommandOutputType.Info, object Sender = null)
        {
            CommandOutputArgs OutputArgs = new CommandOutputArgs()
            {
                OutputMessage = Message,
                CommandOutputType = MessageType
            };

            OnOutput?.Invoke(Sender, OutputArgs);
        }
    }
}
