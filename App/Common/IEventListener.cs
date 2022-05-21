using Gleisbelegung.App.Events;
using PubSub;

namespace Gleisbelegung.App.Common
{
    public interface IEventListener<T> : INonGenericEventListener where T : IEvent
    {
        void ProcessEvent(T eventData);
    }
}