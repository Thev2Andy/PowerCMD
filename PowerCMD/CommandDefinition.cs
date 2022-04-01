using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class CommandDefinition
    {
        public static void Help()
        {
            CommandOutput.Output($"Available commands: ({CommandRegistry.RegisteredCommands.Count})", CommandOutputType.Null);

            for (int i = 0; i < CommandRegistry.RegisteredCommands.Count; i++)
            {
                CommandOutput.Output($"> {CommandRegistry.RegisteredCommands[i].Identifier}", CommandOutputType.Null);
            }
        }

        public static void Echo(string Message)
        {
            CommandOutput.Output(Message, CommandOutputType.Null);
        }

        public static int Add()
        {
            CommandOutput.Output("Adding 3 + 4..", CommandOutputType.Null);
            return 3 + 4;
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static async Task Wait(string Delay)
        {
            CommandOutput.Output($"Waiting {Convert.ToInt32(Delay)} ms..");
            await Task.Delay(Convert.ToInt32(Delay));
            CommandOutput.Output("Command finished.");
        }
    }
}
