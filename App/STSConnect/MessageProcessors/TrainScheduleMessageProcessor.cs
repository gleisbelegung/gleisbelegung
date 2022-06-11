using System;
using System.Globalization;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
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
            var database = Database.GetInstance();
            var train = database.Trains[eventData.Message.Zid];

            foreach (var gleis in eventData.Message.Gleise)
            {
                var plannedPlatform = database.Platforms[gleis.Name];
                var actualPlatform = database.Platforms[gleis.Plan];

                try
                {
                    var scheduleItem = new TrainScheduleItem
                    {
                        ActualPlatform = actualPlatform,
                        PlannedPlatform = plannedPlatform,
                        Departure = TimeSpan.Parse(gleis.Ab, CultureInfo.InvariantCulture),
                        Arrival = TimeSpan.Parse(gleis.An, CultureInfo.InvariantCulture),
                        Flags = gleis.Flags,
                    };
                    train.Schedule.Add(scheduleItem);

                    EventHub.Publish(new TrainScheduleChangedEvent(train, scheduleItem));
                }
                catch (System.Exception e)
                {
                    GD.Print(gleis.Ab + " " + gleis.An);
                    GD.Print(e.Message);
                }
            }
        }
    }
}