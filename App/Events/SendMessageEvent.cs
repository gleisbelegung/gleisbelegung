using Gleisbelegung.App.Common;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.Events
{
    public class SendMessageEvent : IEvent
    {
        public readonly IOutgoingMessage Message;

        public SendMessageEvent(IOutgoingMessage message)
        {
            Message = message;
        }
    }
}