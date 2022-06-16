using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;
using Gleisbelegung.App.STSConnect.MessageProcessors;
using Godot;

namespace Gleisbelegung.App.Extensions
{
    public static class TrainExtensions
    {
        public static TimeSpan? GetActualDeparture(this Train train, TrainScheduleItem trainSchedule)
        {
            TimeSpan? plannedDeparture;

            if (trainSchedule.PlannedDeparture == null)
            {
                var successorFlag = trainSchedule.GetFlagByType(TrainScheduleFlagType.Successor);
                if (successorFlag == null)
                {
                    return null;
                }

                var successorTrain = Database.Instance.Trains[successorFlag.TrainId.Value];

                if (successorTrain == null)
                    return null;

                var firstStopOfSuccessor = successorTrain.Schedule.FirstOrDefault();
                if (firstStopOfSuccessor == null)
                {
                    GD.Print("Train " + successorTrain.Id + " has no schedule (yet)!");
                    return null;
                }

                plannedDeparture = firstStopOfSuccessor.PlannedDeparture;
            }
            else
            {
                plannedDeparture = trainSchedule.PlannedDeparture.Value;
            }

            return plannedDeparture?.Add(train.Delay);
        }

        public static TimeSpan? GetActualArrival(this Train train, TrainScheduleItem trainSchedule)
        {
            return trainSchedule.PlannedArrival?.Add(train.Delay);
        }

        public static string GetDelayAsString(this Train train)
        {
            if (train.Delay.Minutes == 0)
            {
                return "";
            }

            return $" ({(train.Delay.Minutes > 0 ? "+" : "")}{train.Delay.Minutes})";
        }

        public static TrainScheduleFlag GetFlagByType(this TrainScheduleItem trainScheduleItem, TrainScheduleFlagType type)
        {
            return trainScheduleItem.Flags.FirstOrDefault(f => f.Type == type);
        }
    }
}