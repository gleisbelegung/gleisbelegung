using Gleisbelegung.App.Common;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.Events
{
    public class IncomingMessageEvent<T> : IEvent where T : IIncomingMessage
    {
        public T Message { get; }

        public IncomingMessageEvent(T message)
        {
            Message = message;
        }
    }
}