using System;
using System.Reflection;
using PowerCMD;
using PowerLog;
using System.Threading.Tasks;

namespace CMDTester
{
    class Program
    {
        public static ExecutionSystem ExecutionSystem;
        public static Registry Registry;
        public static Parser Parser;

        static void Main(string[] args)
        {
            LogSession.Initialize();
            Logger.OnLog += OnLog;

            Registry = new Registry();
            ExecutionSystem = new ExecutionSystem(Registry);
            Parser = new Parser();

            Output.OnOutput += OnOutput;

            Registry.Register("help", new Action(Help).Method);
            Registry.Register("echo", new Action<string>(Echo).Method);
            Registry.Register("beep", new Action(Console.Beep).Method);
            Registry.Register("clear", new Action(Console.Clear).Method);
            Registry.Register("exit", new Action(Exit).Method);
            Registry.Register("add", new Func<int>(Add).Method);
            Registry.Register("getinput", new Func<string>(GetInput).Method);
            Registry.Register("dummy", new Action<string, string>(DummyAction).Method);
            Registry.Register("exception", new Action(ExceptionF).Method);

            CMDInput:
            Console.Write("> ");
            Parser.Parse(Console.ReadLine(), ExecutionSystem);
            Console.WriteLine();

            goto CMDInput;

            // Registry.IsRegistered("echo", true);

            // Parser.Parse("help");
            // Parser.Parse("echo hello");
            // Parser.Parse("echo 'hello world'");

            // ExecutionSystem.Execute("help");
            // ExecutionSystem.Execute("echo", 3.ToString());
            // ExecutionSystem.Execute("echo", "Hello and welcome to black mesa research facility.");

            // Console.ReadKey();
        }

        public static void ExceptionF()
        {
            throw new AccessViolationException();
        }

        public static void OnLog(object Sender, LogEventArgs LogArgs)
        {
            Console.WriteLine($"{((LogArgs.Timestamped) ? $"[{DateTime.Now.ToString("HH:mm:ss")}] " : "")}" +
                $"{((LogArgs.MessageType != LogType.Null) ? $"{LogArgs.MessageType.ToString()}: " : "")}" +
                $"{LogArgs.LogMessage}");
        }

        public static void OnOutput(object Sender, OutputArgs OutputArgs)
        {
            LogType LogType = OutputArgs.OutputType switch
            {
                OutputType.Info => LogType.Info,
                OutputType.Warning => LogType.Warning,
                OutputType.Error => LogType.Error,
                OutputType.Network => LogType.Network,
                OutputType.NA => LogType.Null,
                _ => LogType.Null,
            };

            Logger.Log(OutputArgs.OutputMessage, LogType);
        }

        public static void DummyAction(string param, string param2)
        {
            Output.Write($"{param} x{Convert.ToInt32(param2).ToString()}", OutputType.NA);
        }

        public static void Help()
        {
            Output.Write($"Available commands: ({Registry.Entries.Count})", OutputType.NA);

            for (int i = 0; i < Registry.Entries.Count; i++)
            {
                Output.Write($"> {Registry.Entries[i].Identifier}", OutputType.NA);
            }
        }

        public static void Echo(string Message)
        {
            Output.Write(Message, OutputType.NA);
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
            Output.Write("Adding 3 + 4..", OutputType.NA);
            return 3 + 4;
        }
    }
}
