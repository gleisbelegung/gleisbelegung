using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class ConnectionStatusLabel : Label, IEventListener<ConnectionStatusEvent>
{
    public override void _Ready()
    {
        this.RegisterSubscriptions();
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        Text = eventData.ConnectionStatus.ToString();
    }
}
