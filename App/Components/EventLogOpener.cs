using Godot;

public class EventLogOpener : Button
{
    private WindowDialog _eventLog;

    public override void _Ready()
    {
        _eventLog = GetTree().Root.GetNode("Spatial").GetNode<WindowDialog>("EventLog");
    }

    public override void _Pressed()
    {
        _eventLog.Popup_();
    }
}
