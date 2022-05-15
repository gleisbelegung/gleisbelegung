using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.Events
{
    public class IncomingMessageEvent<T> where T : IIncomingMessage
    {
        public T Message { get; }

        public IncomingMessageEvent(T message)
        {
            Message = message;
        }
    }
}