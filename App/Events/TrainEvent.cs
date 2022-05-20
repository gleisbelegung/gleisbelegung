using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;
using Gleisbelegung.App.STSConnect.Common;

namespace Gleisbelegung.App.Events
{
    public class TrainEvent : IEvent
    {
        public Train Train { get; set; }
        public TrainEventType Type { get; set; }
    }
}