using System.Collections.Generic;
using Gleisbelegung.App.Data;

namespace Gleisbelegung.App.STSConnect.Common
{
    public class FlagParserType
    {
        public readonly char FlagLetter;
        public readonly bool IsMultiFlag;
        public readonly bool RequiresTrainId;
        public readonly TrainScheduleFlagType FlagType;

        public FlagParserType(char flagLetter, bool isMultiFlag, bool requiresTrainId, TrainScheduleFlagType flagType)
        {
            FlagLetter = flagLetter;
            IsMultiFlag = isMultiFlag;
            RequiresTrainId = requiresTrainId;
            FlagType = flagType;
        }

        public static FlagParserType CanLeaveEarly = new FlagParserType('A', false, false, TrainScheduleFlagType.CanLeaveEarly);
        public static FlagParserType ThemeScript = new FlagParserType('B', true, false, TrainScheduleFlagType.ThemeScript);
        public static FlagParserType PassWithoutStop = new FlagParserType('D', false, false, TrainScheduleFlagType.PassWithoutStop);
        public static FlagParserType Successor = new FlagParserType('E', true, true, TrainScheduleFlagType.Successor);
        public static FlagParserType Split = new FlagParserType('F', true, true, TrainScheduleFlagType.Split);
        public static FlagParserType Join = new FlagParserType('K', true, true, TrainScheduleFlagType.Join);
        public static FlagParserType LocomotiveCirculation = new FlagParserType('L', false, false, TrainScheduleFlagType.LocomotiveCirculation);
        public static FlagParserType CanBeSpawnedHere = new FlagParserType('P', false, false, TrainScheduleFlagType.CanBeSpawnedHere);
        public static FlagParserType ChangeDirection = new FlagParserType('R', false, false, TrainScheduleFlagType.ChangeDirection);
        public static FlagParserType LocomotiveChange = new FlagParserType('W', false, false, TrainScheduleFlagType.LocomotiveChange);

        public static List<FlagParserType> AllFlagParserTypes = new List<FlagParserType>()
        {
            CanLeaveEarly,
            ThemeScript,
            PassWithoutStop,
            Successor,
            Split,
            Join,
            LocomotiveCirculation,
            CanBeSpawnedHere,
            ChangeDirection,
            LocomotiveChange
        };
    }
}