using PubSub;

namespace Gleisbelegung.App.Common
{
    public abstract class EventListener<T> : IEventListener
    {
        public EventListener()
        {
            Hub.Default.Subscribe<T>(ProcessEvent);
        }

        protected abstract void ProcessEvent(T eventData);
    }
}