using System;
using System.Text.RegularExpressions;
using Gleisbelegung.App;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Godot;

public class VersionComponent : ConfirmationDialog, IEventListener<ConnectionStatusEvent>
{
    private RichTextLabel _richTextLabel;

    public override void _Ready()
    {
        this.RegisterSubscriptions();

        if (Updater.HasUpdateCapabilities() && Updater.IsUpdater())
        {
            Updater.UpdateApplication();
        }

        _richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        _richTextLabel.Connect("meta_clicked", this, nameof(OnMetaClicked));
    }

    private void OnMetaClicked(string meta)
    {
        OS.ShellOpen(meta);
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        if (eventData.ConnectionStatus != ConnectionStatus.ESTABLISHED)
            return;

        if (Updater.HasUpdateCapabilities() && !Updater.IsUpdater() && Updater.NeedsUpdate())
        {
            _richTextLabel.BbcodeText += Updater.GetChangelogAsText().ConvertMarkdownToBBCode();

            CallDeferred("popup_centered");
            GetOk().Connect("pressed", this, nameof(OnOkPressed));
        }
    }

    private void OnOkPressed()
    {
        Updater.PrepareAndExecuteUpdater();
    }
}
