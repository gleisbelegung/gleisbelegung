using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;

namespace Gleisbelegung.App.Events
{
    public class NewTrainInformationAvailable : IEvent
    {
        public Train Train { get; set; }
    }
}