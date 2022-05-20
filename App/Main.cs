using System;
using System.Linq;
using System.Threading.Tasks;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect;
using Gleisbelegung.App.STSConnect.MessageProcessors;
using Gleisbelegung.App.STSConnect.Messages;
using Godot;

public class Main : Node, IEventListener<ConnectionStatusEvent>
{

    private STSSocket stsSocket;

    public override void _Ready()
    {
        SubscribeToEvents();

        // auto initialize all message processors
        var interfaceType = typeof(IMessageProcessor);
        var all = AppDomain.CurrentDomain.GetAssemblies()
          .SelectMany(x => x.GetTypes())
          .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
          .Select(x => Activator.CreateInstance(x))
          .ToList();

        stsSocket = new STSSocket();

        GD.Print("Hello World!");
    }

    public override void _Notification(int what)
    {
        if (what == MainLoop.NotificationWmQuitRequest || what == MainLoop.NotificationWmGoBackRequest)
        {
            GD.Print("Quitting");
        }
    }

    public void SubscribeToEvents()
    {
        EventHub.Subscribe<ConnectionStatusEvent>(ProcessEvent);
    }

    public void ProcessEvent(ConnectionStatusEvent eventData)
    {
        if (eventData.ConnectionStatus == ConnectionStatus.REGISTERED)
        {
            StartFetchingData();
        }
    }

    private void StartFetchingData()
    {
        EventHub.Publish(new ConnectionStatusEvent(ConnectionStatus.FETCHING_INITIAL_DATA));
        EventHub.Publish(new SendMessageEvent(new FacilityInfoMessage()));
        EventHub.Publish(new SendMessageEvent(new PlatformListMessage()));
        EventHub.Publish(new SendMessageEvent(new TrainListMessage()));
        EventHub.Publish(new SendMessageEvent(new FacilityPathMessage()));
        EventHub.Publish<SendMessageEvent>(new SendMessageEvent(TimeMessageProcessor.CreateTimeMessage()));

    }
}
