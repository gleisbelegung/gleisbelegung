using Gleisbelegung.App.Events;
using PubSub;

namespace Gleisbelegung.App.Common
{
    public interface IEventListener<T> where T : IEvent
    {
        void SubscribeToEvents();
        void ProcessEvent(T eventData);
    }
}