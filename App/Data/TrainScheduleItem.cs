using Gleisbelegung.App.Data;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TrainScheduleItem
    {
        public Platform ActualPlatform { get; set; }
        public Platform PlannedPlatform { get; set; }
        public string Flags { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }
}