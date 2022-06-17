using Gleisbelegung.App;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.MessageProcessors;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

public class Main : Node, IEventListener<ConnectionStatusEvent>
{
    public override void _Ready()
    {
        this.RegisterSubscriptions();

        // auto initialize all message processors
        ReflectionHelper.CreateInstancesByInterface<IMessageProcessor>();

        if (ComputerPlatforms.IsDesktopPlatform(OS.GetName()))
        {
            // OS.LowProcessorUsageMode = true;
            // OS.LowProcessorUsageModeSleepUsec = GleisbelegungDefaults.TargetFPSDuringLowProcessorMode;
        }
    }

    public override void _Notification(int what)
    {
        if (what == MainLoop.NotificationWmQuitRequest || what == MainLoop.NotificationWmGoBackRequest)
        {
            GD.Print("Quitting");
        }
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        switch (eventData.ConnectionStatus)
        {
            case ConnectionStatus.REGISTERED:
                StartFetchingData();
                break;
            case ConnectionStatus.REFETCHING_TRAIN_DETAILS:
                RefetchTrainDetails();
                break;

            default:
                break;
        }
    }

    private void RefetchTrainDetails()
    {
        EventHub.Publish(new SendMessageEvent(new TrainListMessage()));
    }

    private void StartFetchingData()
    {
        EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.FETCHING_INITIAL_DATA));
        EventHub.Publish(new SendMessageEvent(new FacilityInfoMessage()));
        EventHub.Publish(new SendMessageEvent(new PlatformListMessage()));
        EventHub.Publish(new SendMessageEvent(new TrainListMessage()));
        EventHub.Publish(new SendMessageEvent(new FacilityPathMessage()));
        EventHub.Publish(new SendMessageEvent(TimeMessageProcessor.CreateTimeMessage()));
    }
}
