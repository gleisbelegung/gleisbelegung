using System;
using System.Collections.Generic;
using Gleisbelegung.App.STSConnect.MessageProcessors;

namespace Gleisbelegung.App.Data
{
    public class Train
    {
        public Train()
        {
            Schedule = new List<TrainScheduleItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Delay { get; set; }
        public Platform Platform { get; set; }
        public Platform PlannedPlatform { get; set; }
        public bool AtPlatform { get; set; }
        public bool Visible { get; set; }
        public string Destination { get; set; }
        public string StartingPoint { get; set; }
        public List<TrainScheduleItem> Schedule { get; set; }
    }
}