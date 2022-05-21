using System;
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
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<TrainListMessage> eventData)
        {
            foreach (var trainData in eventData.Message.Trains)
            {
                ProcessTrainData(trainData);
            }
        }

        private void ProcessTrainData(TrainListMessage.Train trainData)
        {
            var database = Database.GetInstance();
            if (!database.Trains.ContainsKey(trainData.Zid))
            {
                var newTrain = new Train
                {
                    Id = trainData.Zid,
                    Name = trainData.Name,
                };
                database.Trains.Add(newTrain.Id, newTrain);
                EventHub.Publish<NewTrainInformationAvailable>(new NewTrainInformationAvailable { Train = newTrain });

                RegisterTrainEvents(newTrain);
            }

            var train = database.Trains[trainData.Zid];
            // do whatever / update the train

        }

        private void RegisterTrainEvents(Train newTrain)
        {
            foreach (var trainEvent in TrainEventType.AllTrainEvents)
            {
                EventHub.Publish<SendMessageEvent>(new SendMessageEvent(new TrainEventMessage
                {
                    Art = trainEvent.EventName,
                    Zid = newTrain.Id,
                }));
            }
        }
    }
}