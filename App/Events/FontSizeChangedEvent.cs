using Gleisbelegung.App.Common;

namespace Gleisbelegung.App.Events
{
    public class FontSizeChangedEvent : IEvent
    {
        public int NewFontSize { get; set; }
    }
}