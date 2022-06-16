using Gleisbelegung.App.Common;

namespace Gleisbelegung.App.Events
{
    public class ConnectionStatusEvent : IEvent
    {
        public ConnectionStatus ConnectionStatus { get; }

        public ConnectionStatusEvent(ConnectionStatus connectionStatus)
        {
            Database.Instance.ConnectionsStatus = connectionStatus;
            ConnectionStatus = connectionStatus;
        }
    }
}