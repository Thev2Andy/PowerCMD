using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public static class Output
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



        [Serializable] public class Arguments : EventArgs {
            public string Message;
            public Severity Severity;
            public Object Sender;
        }



        [Serializable] public enum Severity
        {
            Verbose,
            Trace,
            Debug,
            Information,
            Network,
            Notice,
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
