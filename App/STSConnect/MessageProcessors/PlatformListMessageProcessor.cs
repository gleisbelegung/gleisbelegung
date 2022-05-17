using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class PlatformListMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<PlatformListMessage>>
    {
        public PlatformListMessageProcessor()
        {
            SubscribeToEvents();
        }

        public void ProcessEvent(IncomingMessageEvent<PlatformListMessage> eventData)
        {

        }

        public void SubscribeToEvents()
        {
            EventHub.Subscribe<IncomingMessageEvent<PlatformListMessage>>(ProcessEvent);
        }
    }
}