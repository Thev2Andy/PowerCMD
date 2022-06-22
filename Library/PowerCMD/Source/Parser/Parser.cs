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

            for (int i = 0; i < GroupedParameterList.Count; i++)
            {
                if (GroupedParameterList[i].ToString().Contains(Newline)) {
                    GroupedParameterList[i] = GroupedParameterList[i].ToString().Replace(Newline, Environment.NewLine);
                }

                if (GroupedParameterList[i].ToString() == $"{Separator}{Separator}") {
                    GroupedParameterList[i] = GroupedParameterList[i].ToString().Replace($"{Separator}{Separator}", String.Empty);
                }
            }

            return ExecutionSystem.Execute(CommandString, GroupedParameterList.ToArray());
        }



        public Parser(string ParameterGrouper = "`", string Separator = " ", string Newline = "%NL%") {
            this.Separator = Separator;
            this.Newline = Newline;
            Grouper = ParameterGrouper;
            Initialized = true;
        }
    }
}
