using System;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class FontSize : SpinBox
{
    public override void _Ready()
    {
        Value = Database.Instance.Settings.FontSize;
        Connect("value_changed", this, nameof(ValueChanged));
    }

    public void ValueChanged(float inputValue)
    {
        Database.Instance.Settings.FontSize = (int)inputValue;

        EventHub.Publish(new SettingsChangedEvent());
    }
}
