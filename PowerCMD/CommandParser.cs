using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerCMD
{
    public static class CommandParser
    {
        public static string ParameterGrouper { get; private set; }
        public static string Separator { get; private set; }


        public static bool Initialized { get; private set; }

        public static void InitializeParser(string CMDParameterGrouper = "`", string CMDSeparator = " ")
        {
            ParameterGrouper = CMDParameterGrouper;
            Separator = CMDSeparator;
            Initialized = true;
        }

        public static CommandReturn ParseCommand(string Command)
        {
            List<string> SplitCommand = Command.Split(new string[] { Separator }, StringSplitOptions.RemoveEmptyEntries).
                ToList<string>();

            string CommandString = SplitCommand[0];
            SplitCommand.RemoveAt(0);

            string ParametersToParse = string.Join(Separator, SplitCommand.ToArray());

            List<object> GroupedParameterList = new List<object>();
            GroupedParameterList = ParametersToParse.Split(ParameterGrouper).
                Select((Element, Index) => ((Index % 2 == 0) ? Element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { Element })).
                SelectMany(Element => Element).
                ToList().
                ConvertAll(S => (object)S);

            GroupedParameterList.ForEach((S) => { 
                if(S.ToString() == $"{Separator}{Separator}") {
                    S = S.ToString().Replace($"{Separator}{Separator}", String.Empty);
                }
            });

            return CommandExecutionSystem.ExecuteCommand(CommandString, GroupedParameterList.ToArray());
        }
    }
}
