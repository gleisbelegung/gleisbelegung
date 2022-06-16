using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TrainEventMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<TrainEventMessage>>
    {
        public TrainEventMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<TrainEventMessage> eventData)
        {
            // convert to more meaningful event
            var data = eventData.Message;
            TrainEventType eventType = null;
            foreach (var type in TrainEventType.AllTrainEvents)
            {
                if (type.EventName == data.Art)
                {
                    eventType = type;
                    break;
                }
            }

            var train = Database.Instance.Trains[data.Zid];

            EventHub.Publish<TrainEvent>(new TrainEvent
            {
                Train = train,
                Type = eventType,
            });
        }
    }
}