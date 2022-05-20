namespace Gleisbelegung.App.Common
{
    public enum ConnectionStatus
    {
        CONNECTING,
        CONNECTED,
        REGISTERING,
        REGISTERED,
        FETCHING_INITIAL_DATA,
        ESTABLISHED,
    }
}