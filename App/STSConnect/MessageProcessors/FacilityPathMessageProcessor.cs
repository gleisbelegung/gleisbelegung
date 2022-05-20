using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class FacilityPathMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<FacilityPathMessage>>
    {
        public FacilityPathMessageProcessor()
        {
            SubscribeToEvents();
        }

        public void ProcessEvent(IncomingMessageEvent<FacilityPathMessage> eventData)
        {

        }

        public void SubscribeToEvents()
        {
            EventHub.Subscribe<IncomingMessageEvent<FacilityPathMessage>>(ProcessEvent);
        }
    }
}