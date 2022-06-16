using System;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class FontController : Control, IEventListener<SettingsChangedEvent>
{
    private readonly DynamicFont _font;

    public FontController()
    {
        this.RegisterSubscriptions();
        _font = (DynamicFont)GetFont("font");

        Database.Instance.Settings.FontSize = _font.Size;
        EventHub.Publish(new SettingsChangedEvent());
    }

    public void ProcessEvent(SettingsChangedEvent eventData)
    {
        _font.Size = Database.Instance.Settings.FontSize;
    }
}
