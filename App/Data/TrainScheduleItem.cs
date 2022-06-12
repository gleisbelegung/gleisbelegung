using System;
using System.Collections.Generic;
using Gleisbelegung.App.Data;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TrainScheduleItem
    {
        public Platform ActualPlatform { get; set; }
        public Platform PlannedPlatform { get; set; }
        public List<TrainScheduleFlag> Flags { get; set; }
        public TimeSpan? PlannedDeparture { get; set; }
        public TimeSpan? PlannedArrival { get; set; }
    }
}