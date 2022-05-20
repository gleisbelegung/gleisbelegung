using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TrainListMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<TrainListMessage>>
    {
        public TrainListMessageProcessor()
        {
            SubscribeToEvents();
        }

        public void ProcessEvent(IncomingMessageEvent<TrainListMessage> eventData)
        {
            foreach (var trainData in eventData.Message.Trains)
            {
                var train = new Train
                {
                    Id = trainData.Zid,
                    Name = trainData.Name,
                };
                EventHub.Publish<NewTrainInformationAvailable>(new NewTrainInformationAvailable { Train = train });

                foreach (var trainEvent in TrainEventType.AllTrainEvents)
                {
                    EventHub.Publish<SendMessageEvent>(new SendMessageEvent(new TrainEventMessage
                    {
                        Art = trainEvent.EventName,
                        Zid = train.Id,
                    }));
                }
            }
        }

        public void SubscribeToEvents()
        {
            EventHub.Subscribe<IncomingMessageEvent<TrainListMessage>>(ProcessEvent);
        }
    }
}