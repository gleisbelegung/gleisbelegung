using System;
using Gleisbelegung.App.Extensions;
using Godot;
public class SettingsOpener : Button
{
    private WindowDialog _settings;

    public override void _Ready()
    {
        _settings = this.GetRoot().GetNode<WindowDialog>("Settings");
    }

    public override void _Pressed()
    {
        _settings.PopupCentered();
    }
}
