using System;
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
            var database = Database.GetInstance();
            var train = database.Trains[eventData.Message.Zid];
            var platforms = database.Platforms;

            var data = eventData.Message;
            train.Delay = TimeSpan.FromMinutes(data.Verspaetung);
            train.Platform = platforms[data.Gleis];
            train.PlannedPlatform = platforms[data.Plangleis];
            train.AtPlatform = data.Amgleis;
            train.Visible = data.Sichtbar;
            train.Destination = data.Nach;
            train.StartingPoint = data.Von;
        }
    }
}