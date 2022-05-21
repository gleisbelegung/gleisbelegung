using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class StatusMessageProcessor : IEventListener<IncomingMessageEvent<StatusMessage>>, IMessageProcessor
    {
        public StatusMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<StatusMessage> eventData)
        {
            var message = eventData.Message;

            if (message.Code == 300)
            {
                var registerMessage = new RegisterMessage
                {
                    Autor = "Manuel Serret",
                    Name = "Gleisbelegung",
                    Version = "1.0",
                    Protokoll = 1,
                    Text = "Tut nix!"
                };
                EventHub.Publish(new SendMessageEvent(registerMessage));
                EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.REGISTERING));
            }
            else if (message.Code == 220)
            {
                EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.REGISTERED));
            }
        }
    }
}