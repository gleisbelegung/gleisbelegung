using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;
using Gleisbelegung.App.STSConnect.MessageProcessors;

namespace Gleisbelegung.App.Events
{
    public class TrainScheduleChangedEvent : IEvent
    {
        public Train Train { get; }
        public TrainScheduleItem TrainSchedule { get; }

        public TrainScheduleChangedEvent(Train train, TrainScheduleItem trainSchedule)
        {
            Train = train;
            TrainSchedule = trainSchedule;
        }
    }
}