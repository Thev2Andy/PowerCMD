using System;
using System.Reflection;
using System.Threading.Tasks;
using PowerCMD;

namespace CMDTester
{
    class Program
    {
        public static ExecutionSystem ExecutionSystem;
        public static Registry Registry;
        public static Parser Parser;

        static void Main(string[] Args)
        {
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
            Registry.Register("exception", new Action(ThrowException).Method);

            CMDInput:
            Console.Write("> ");
            Parser.Parse(Console.ReadLine(), ExecutionSystem);
            Console.WriteLine();

            goto CMDInput;
        }

        public static void ThrowException() {
            throw new AccessViolationException();
        }

        public static void OnOutput(Output.Arguments OutputArguments) {
            Console.WriteLine($"{((OutputArguments.Severity != Output.Severity.Generic) ? $"{OutputArguments.Severity}: " : "")}" + $"{OutputArguments.Message}");
        }

        public static void DummyAction(string Parameter, string ParameterTwo) {
            Output.Write($"{Parameter} x{Convert.ToInt32(ParameterTwo)}", Output.Severity.Generic);
        }

        public static void Help()
        {
            Output.Write($"Available commands: ({Registry.Entries.Count})", Output.Severity.Generic);

            for (int I = 0; I < Registry.Entries.Count; I++)
            {
                Output.Write($"> {Registry.Entries[I].Identifier}", Output.Severity.Generic);
            }
        }

        public static void Echo(string Message) {
            Output.Write(Message, Output.Severity.Generic);
        }

        public static string GetInput() {
            return Console.ReadLine();
        }

        public static void Exit() {
            Environment.Exit(0);
        }

        public static int Add()
        {
            Output.Write("Adding 3 + 4..", Output.Severity.Generic);
            return 3 + 4;
        }
    }
}
