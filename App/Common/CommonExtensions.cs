using Gleisbelegung.App.Events;

namespace Gleisbelegung.App.Common
{
    public static class CommonExtensions
    {
        public static void RegisterSubscriptions(this INonGenericEventListener listener)
        {
            EventHub.RegisterSubscriptions(listener);
        }
    }
}