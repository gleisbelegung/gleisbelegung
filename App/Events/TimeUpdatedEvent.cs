using System;
using Gleisbelegung.App.Common;

namespace Gleisbelegung.App.Events
{
    public class TimeUpdatedEvent : IEvent
    {
        public DateTime Time { get; }

        public TimeUpdatedEvent(DateTime time)
        {
            Time = time;
        }
    }
}