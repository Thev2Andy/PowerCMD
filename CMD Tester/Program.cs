using System;
using System.Reflection;
using PowerCMD;
using PowerLog;
using System.Threading.Tasks;

namespace CMD_Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            LogSession.Initialize(false);
            Log.OnLog += OnLog;

            CommandParser.InitializeParser();

            CommandOutput.OnOutput += OnOutput;

            CommandRegistry.RegisterCommand("help", new Action(Help).Method);
            CommandRegistry.RegisterCommand("echo", new Action<string>(Echo).Method);
            CommandRegistry.RegisterCommand("beep", new Action(Console.Beep).Method);
            CommandRegistry.RegisterCommand("clear", new Action(Console.Clear).Method);
            CommandRegistry.RegisterCommand("exit", new Action(Exit).Method);
            CommandRegistry.RegisterCommand("add", new Func<int>(Add).Method);
            CommandRegistry.RegisterCommand("getinput", new Func<string>(GetInput).Method);
            CommandRegistry.RegisterCommand("dummy", new Action<string, string>(DummyAction).Method);
            CommandRegistry.RegisterCommand("exception", new Action(ExceptionF).Method);

            CommandRegistry.IsCommandRegistered("echo").Execute("h", "o");

            CMDInput:
            Console.Write("> ");
            CommandParser.ParseCommand(Console.ReadLine());
            Console.WriteLine();

            goto CMDInput;

            // CommandRegistry.IsCommandRegistered("echo", true);

            // CommandParser.ParseCommand("help");
            // CommandParser.ParseCommand("echo hello");
            // CommandParser.ParseCommand("echo 'hello world'");

            // CommandExecutionSystem.ExecuteCommand("help");
            // CommandExecutionSystem.ExecuteCommand("echo", 3.ToString());
            // CommandExecutionSystem.ExecuteCommand("echo", "Hello and welcome to black mesa research facility.");

            // Console.ReadKey();
        }

        public static void ExceptionF()
        {
            throw new AccessViolationException();
        }

        public static void OnLog(object Sender, LogArgs LogArgs)
        {
            Console.WriteLine($"{((LogArgs.LoggingMode.HasFlag(LogMode.Timestamp)) ? $"[{DateTime.Now.ToString("HH:mm:ss")}] " : "")}" +
                $"{((LogArgs.LogLevel != LogType.Null) ? $"{LogArgs.LogLevel.ToString()}: " : "")}" +
                $"{LogArgs.LogMessage}");
        }

        public static void OnOutput(object Sender, CommandOutputArgs OutputArgs)
        {
            LogType LogType = OutputArgs.CommandOutputType switch
            {
                CommandOutputType.Info => LogType.Info,
                CommandOutputType.Warning => LogType.Warning,
                CommandOutputType.Error => LogType.Error,
                CommandOutputType.Network => LogType.Network,
                CommandOutputType.Null => LogType.Null,
                _ => LogType.Null,
            };

            Log.LogL(OutputArgs.OutputMessage, LogType, LogMode.Default);
        }

        public static void DummyAction(string param, string param2)
        {
            CommandOutput.Output($"{param} x{Convert.ToInt32(param2).ToString()}", CommandOutputType.Null);
        }

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

        public static string GetInput()
        {
            return Console.ReadLine();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static int Add()
        {
            CommandOutput.Output("Adding 3 + 4..", CommandOutputType.Null);
            return 3 + 4;
        }
    }
}
