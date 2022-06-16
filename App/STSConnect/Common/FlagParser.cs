using System.Collections.Generic;
using Gleisbelegung.App.Data;
using Godot;

namespace Gleisbelegung.App.STSConnect.Common
{
    public class FlagParser
    {
        public static List<TrainScheduleFlag> ParseFlags(string flags)
        {
            var flagList = new List<TrainScheduleFlag>();

            if (string.IsNullOrEmpty(flags))
            {
                return flagList;
            }

            var flagArray = flags.ToCharArray();
            var allFlagTypes = FlagParserType.AllFlagParserTypes;

            for (var i = 0; i < flagArray.Length; i++)
            {
                var flagLetter = flagArray[i];

                foreach (var flagType in allFlagTypes)
                {
                    if (flagType.FlagLetter != flagLetter)
                    {
                        continue;
                    }

                    int? trainId = null;

                    if (flagType.RequiresTrainId)
                    {
                        var openingBracket = GetNextOccurrence(flagArray, i, '(');
                        var closingBracket = GetNextOccurrence(flagArray, i, ')');

                        trainId = int.Parse(flags.Substring(openingBracket + 1, closingBracket - openingBracket - 1));

                        i = closingBracket;
                    }

                    // workaround for that weird flag syntax
                    // see documentation for p and w flags for details
                    if (flagType == FlagParserType.CanBeSpawnedHere
                        || flagType == FlagParserType.LocomotiveChange)
                    {
                        var newI = GetNextOccurrence(flagArray, i, ']');

                        if (flagType == FlagParserType.LocomotiveChange)
                        {
                            newI = GetNextOccurrence(flagArray, newI, ']');
                        }

                        // sometimes a P tag has braces, sometimes not. This fixes the behavior.
                        if (newI > 0)
                        {
                            i = newI;
                        }
                    }

                    flagList.Add(new TrainScheduleFlag
                    {
                        Type = flagType.FlagType,
                        TrainId = trainId
                    });
                }
            }

            return flagList;
        }

        public static int GetNextOccurrence(char[] flagArray, int startIndex, char charToFind)
        {
            for (var i = startIndex; i < flagArray.Length; i++)
            {
                if (flagArray[i] == charToFind)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}