using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class FacilityPathMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<FacilityPathMessage>>
    {
        public FacilityPathMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<FacilityPathMessage> eventData)
        {

        }
    }
}