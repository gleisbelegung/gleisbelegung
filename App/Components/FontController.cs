using System;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class FontController : Control, IEventListener<FontSizeChangedEvent>
{
    private readonly DynamicFont _font;

    public FontController()
    {
        this.RegisterSubscriptions();
        _font = (DynamicFont)GetFont("font");
    }

    public void ProcessEvent(FontSizeChangedEvent eventData)
    {
        _font.Size = eventData.NewFontSize;
    }
}
