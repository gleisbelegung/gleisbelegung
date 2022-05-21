using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class FacilityInfoMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<FacilityInfoMessage>>
    {
        public FacilityInfoMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<FacilityInfoMessage> eventData)
        {
            // EventHub.Publish(new SendMessageEvent(new PlatformListMessage()));

        }
    }
}