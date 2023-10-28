using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class Output
    {
        public static Action<Arguments> OnOutput;

        public static void Write(string Message, Severity MessageType = Severity.Information, Object Sender = null)
        {
            Arguments OutputArgs = new Arguments() {
                Message = Message,
                Severity = MessageType,
                Sender = Sender
            };

            OnOutput?.Invoke(OutputArgs);
        }



        public class Arguments {
            public string Message;
            public Severity Severity;
            public Object Sender;
        }



        public enum Severity
        {
            Verbose,
            Trace,
            Debug,
            Information,
            Network,
            Notice,
            Caution,
            Warning,
            Alert,
            Error,
            Critical,
            Emergency,
            Fatal,
            Generic,
        }
    }
}
