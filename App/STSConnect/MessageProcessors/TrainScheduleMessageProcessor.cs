using System;
using System.Globalization;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TrainScheduleMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<TrainScheduleMessage>>
    {
        public TrainScheduleMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<TrainScheduleMessage> eventData)
        {
            var database = Database.Instance;
            var train = database.Trains[eventData.Message.Zid];
            database.ReceivedTrainSchedules++;

            foreach (var gleis in eventData.Message.Gleise)
            {
                var plannedPlatform = database.Platforms[gleis.Name];
                var actualPlatform = database.Platforms[gleis.Plan];

                var scheduleItem = new TrainScheduleItem
                {
                    ActualPlatform = actualPlatform,
                    PlannedPlatform = plannedPlatform,
                    PlannedDeparture = string.IsNullOrEmpty(gleis.Ab) ? null : TimeSpan.Parse(gleis.Ab, CultureInfo.InvariantCulture),
                    PlannedArrival = string.IsNullOrEmpty(gleis.An) ? null : TimeSpan.Parse(gleis.An, CultureInfo.InvariantCulture),
                    Flags = FlagParser.ParseFlags(gleis.Flags),
                };
                train.Schedule.Add(scheduleItem);

                EventHub.Publish(new TrainScheduleChangedEvent(train, scheduleItem));
            }

            GD.Print(database.ReceivedTrainSchedules + " from " + database.Trains.Count);
            if (database.ReceivedTrainSchedules == database.Trains.Count)
            {
                if (database.ConnectionsStatus == ConnectionStatus.REFETCHING_TRAIN_DETAILS)
                {
                    EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.ESTABLISHED));
                }
                else if (database.ConnectionsStatus == ConnectionStatus.FETCHING_INITIAL_DATA)
                {
                    // after the start we need to get the train list twice, in order to be able to successfully determine the successors
                    EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.REFETCHING_TRAIN_DETAILS));
                }
            }
        }
    }
}