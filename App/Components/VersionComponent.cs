using Gleisbelegung.App;
using Godot;

public class VersionComponent : ConfirmationDialog
{
    public override void _Ready()
    {
        if (Updater.HasUpdateCapabilities() && !Updater.IsUpdater() && Updater.NeedsUpdate())
        {
            DialogText += Updater.GetChangelogAsText();

            CallDeferred("popup");
            GetOk().Connect("pressed", this, nameof(OnOkPressed));
        }
        else if (Updater.HasUpdateCapabilities() && Updater.IsUpdater())
        {
            Updater.UpdateApplication();
        }
    }

    private void OnOkPressed()
    {
        Updater.PrepareAndExecuteUpdater();
    }
}
