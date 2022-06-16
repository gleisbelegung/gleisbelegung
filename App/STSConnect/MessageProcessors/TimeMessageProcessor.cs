using System;
using System.Threading.Tasks;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class TimeMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<TimeMessage>>
    {
        private bool isFirstUpdate = true;
        private int forceRefreshCount = 0;
        private int forceRefreshGuideline = 15;
        private DateTime simTime;

        public TimeMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<TimeMessage> eventData)
        {
            var roundTime = GetTodaysTime() - eventData.Message.Sender;
            var time = eventData.Message.Zeit + roundTime / 2;

            simTime = DateTime.Today.Add(TimeSpan.FromMilliseconds(time));
            var database = Database.Instance;
            database.Time = simTime;

            if (isFirstUpdate)
            {
                EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.FETCHING_INITIAL_DATA));
                isFirstUpdate = false;

                var task = Task.Run(async () =>
               {
                   while (true)
                   {
                       await Task.Delay(1000);
                       simTime = simTime.Add(TimeSpan.FromSeconds(1));
                       database.Time = simTime;
                       EventHub.Publish(new TimeUpdatedEvent(simTime));

                       forceRefreshCount++;

                       if (forceRefreshCount >= forceRefreshGuideline)
                       {
                           EventHub.Publish(new SendMessageEvent(CreateTimeMessage()));
                           forceRefreshCount = 0;
                       }
                   }
               });
            }
        }

        public static TimeMessage CreateTimeMessage()
        {
            var timeMessage = new TimeMessage();
            timeMessage.Sender = GetTodaysTime(); ;
            return timeMessage;
        }

        private static int GetTodaysTime()
        {
            return (int)(DateTime.Now - DateTime.Today).TotalMilliseconds;
        }
    }
}