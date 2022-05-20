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
            SubscribeToEvents();
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


            EventHub.Publish<TrainEvent>(new TrainEvent
            {
                // Train = data.Zid,
                Type = eventType,
            });
        }

        public void SubscribeToEvents()
        {
            EventHub.Subscribe<IncomingMessageEvent<TrainEventMessage>>(ProcessEvent);
        }
    }
}