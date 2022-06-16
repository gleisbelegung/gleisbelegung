using System;
using Gleisbelegung.App.Events;
using Godot;

public class FontSize : LineEdit
{
    public override void _Ready()
    {
        Connect("text_changed", this, nameof(TextChanged));
    }

    public void TextChanged(string newText)
    {
        var parsed = int.Parse(newText);
        EventHub.Publish(new FontSizeChangedEvent
        {
            NewFontSize = parsed,
        });
    }
}
