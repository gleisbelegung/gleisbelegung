using Gleisbelegung.App;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class TimeLabel : Label, IEventListener<TimeUpdatedEvent>
{
    private string _currentVersion;

    public TimeLabel()
    {
        this.RegisterSubscriptions();
        _currentVersion = Updater.GetCurrentVersion();
    }

    public void ProcessEvent(TimeUpdatedEvent eventData)
    {
        Text = eventData.Time.ToString("HH:mm:ss") + " " + Engine.GetFramesPerSecond() + "FPS " + _currentVersion;
    }
}
