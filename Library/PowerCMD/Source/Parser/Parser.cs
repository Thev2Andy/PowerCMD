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
        public string Newline { get; private set; }

        public bool Initialized { get; private set; }


        public Return Parse(string Command, ExecutionSystem ExecutionSystem)
        {
            List<string> SplitCommand = Command.Split(new string[] { Separator }, StringSplitOptions.RemoveEmptyEntries).
                ToList<string>();

            string CommandString = SplitCommand[0];
            SplitCommand.RemoveAt(0);

            string ParametersToParse = String.Join(Separator, SplitCommand.ToArray());

            List<Object> GroupedParameterList = new List<Object>();
            GroupedParameterList = ParametersToParse.Split(Grouper).
                Select((Element, Index) => ((Index % 2 == 0) ? Element.Split(Separator, StringSplitOptions.RemoveEmptyEntries) : new string[] { Element })).
                SelectMany(Element => Element).
                ToList().
                ConvertAll(S => (Object)S);

            for (int I = 0; I < GroupedParameterList.Count; I++)
            {
                if (GroupedParameterList[I].ToString().Contains(Newline)) {
                    GroupedParameterList[I] = GroupedParameterList[I].ToString().Replace(Newline, Environment.NewLine);
                }

                if (GroupedParameterList[I].ToString() == $"{Separator}{Separator}") {
                    GroupedParameterList[I] = GroupedParameterList[I].ToString().Replace($"{Separator}{Separator}", String.Empty);
                }
            }

            return ExecutionSystem.Execute(CommandString, GroupedParameterList.ToArray());
        }



        public Parser(string ParameterGrouper = "`", string Separator = " ", string Newline = @"\n") {
            this.Separator = Separator;
            this.Newline = Newline;
            Grouper = ParameterGrouper;
            Initialized = true;
        }
    }
}
