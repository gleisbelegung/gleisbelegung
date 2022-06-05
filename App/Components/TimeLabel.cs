using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class TimeLabel : Label, IEventListener<TimeUpdatedEvent>
{
    public TimeLabel()
    {
        this.RegisterSubscriptions();
    }

    public void ProcessEvent(TimeUpdatedEvent eventData)
    {
        Text = eventData.Time.ToString("HH:mm:ss") + " " + Engine.GetFramesPerSecond() + "FPS " + Gleisbelegung.App.Version.ToString();
    }
}
