using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TrainDetailsMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<TrainDetailsMessage>>
    {
        public TrainDetailsMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<TrainDetailsMessage> eventData)
        {


        }
    }
}