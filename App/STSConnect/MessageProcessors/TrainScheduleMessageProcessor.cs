using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

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

                train.Schedule.Add(new TrainScheduleItem
                {
                    ActualPlatform = actualPlatform,
                    PlannedPlatform = plannedPlatform,
                    Departure = gleis.Ab,
                    Arrival = gleis.An,
                    Flags = gleis.Flags,
                });
            }
        }
    }
}