using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    [Serializable] public class Parser
    {
        public string Separator { get; private set; }
        public string Grouper { get; private set; }

        public bool Initialized { get; private set; }


        public Parser(string ParameterGrouper = "`", string Separator = " ")
        {
            this.Separator = Separator;
            Grouper = ParameterGrouper;
            Initialized = true;
        }

        public Return Parse(string Command, ExecutionSystem ExecutionSystem)
        {
            List<string> SplitCommand = Command.Split(new string[] { Separator }, StringSplitOptions.RemoveEmptyEntries).
                ToList<string>();

            string CommandString = SplitCommand[0];
            SplitCommand.RemoveAt(0);

            string ParametersToParse = String.Join(Separator, SplitCommand.ToArray());

            List<object> GroupedParameterList = new List<object>();
            GroupedParameterList = ParametersToParse.Split(Grouper).
                Select((Element, Index) => ((Index % 2 == 0) ? Element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { Element })).
                SelectMany(Element => Element).
                ToList().
                ConvertAll(S => (object)S);

            GroupedParameterList.ForEach((S) => { 
                if(S.ToString() == $"{Separator}{Separator}") {
                    S = S.ToString().Replace($"{Separator}{Separator}", String.Empty);
                }
            });

            return ExecutionSystem.Execute(CommandString, GroupedParameterList.ToArray());
        }
    }
}
