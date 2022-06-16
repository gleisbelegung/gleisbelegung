using System;
using System.Drawing.Text;
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

            Database.Instance.ReceivedTrainSchedules = 0;
            RequestTrainDetails();
        }

        private void RequestTrainDetails()
        {
            var trains = Database.Instance.Trains;

            foreach (var trainKeyValue in trains)
            {
                var train = trainKeyValue.Value;

                EventHub.Publish(new SendMessageEvent(new TrainDetailsMessage { Zid = train.Id }));
                EventHub.Publish(new SendMessageEvent(new TrainScheduleMessage { Zid = train.Id }));
            }
        }

        private void ProcessTrainData(TrainListMessage.Train trainData)
        {
            var database = Database.Instance;
            if (!database.Trains.ContainsKey(trainData.Zid))
            {
                if (trainData.Zid <= 0)
                {
                    // TODO: Ersatzloks have no Id and platform. So they destroy a lot of our assumptions.
                    // That's why we ignore them for the moment.
                    return;
                }

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
            train.Name = trainData.Name;
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