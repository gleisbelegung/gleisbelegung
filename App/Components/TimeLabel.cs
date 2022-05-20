using System;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class TimeLabel : Label, IEventListener<TimeUpdatedEvent>
{
    public TimeLabel()
    {
        SubscribeToEvents();
    }

    public void ProcessEvent(TimeUpdatedEvent eventData)
    {
        Text = eventData.Time.ToString("HH:mm:ss");
    }

    public void SubscribeToEvents()
    {
        EventHub.Subscribe<TimeUpdatedEvent>(ProcessEvent);
    }
}
