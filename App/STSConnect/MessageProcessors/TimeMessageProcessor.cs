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
            var roundTime = (int)(DateTime.Now - DateTime.Today).TotalMilliseconds - eventData.Message.Sender;
            var time = eventData.Message.Zeit + roundTime / 2;

            simTime = DateTime.Today.Add(TimeSpan.FromMilliseconds(time));

            if (isFirstUpdate)
            {
                EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.ESTABLISHED));
                isFirstUpdate = false;

                var task = Task.Run(async () =>
               {
                   while (true)
                   {
                       await Task.Delay(1000);
                       simTime = simTime.Add(TimeSpan.FromSeconds(1));
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
            timeMessage.Sender = (int)(DateTime.Now - DateTime.Today).TotalMilliseconds;
            return timeMessage;
        }
    }
}