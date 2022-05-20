using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Godot;

public class EventLog : Label, IEventListener<NewTrainInformationAvailable>, IEventListener<TrainEvent>
{
    public EventLog()
    {
        SubscribeToEvents();
    }

    public void ProcessEvent(NewTrainInformationAvailable eventData)
    {
        // Text = Text + $"{eventData.Train.Name} arrived at FacilityName\n";
    }

    public void ProcessEvent(TrainEvent eventData)
    {
        if (eventData.Type == TrainEventType.Incoming)
        {
            // Text += "Train arrived at facility\n";
        }
        else if (eventData.Type == TrainEventType.Arrival)
        {
            Text += "Train arrived at a platform\n";
        }
        else if (eventData.Type == TrainEventType.Departure)
        {
            // Text += "Train departed at a platform\n";
        }
        else if (eventData.Type == TrainEventType.RedSignal)
        {
            Text += "Train has a red signal\n";
        }
        else if (eventData.Type == TrainEventType.BecameGreen)
        {
            Text += "Train became green\n";
        }
        else if (eventData.Type == TrainEventType.Couple)
        {
            Text += "Train coupled\n";
        }
        else if (eventData.Type == TrainEventType.Wing)
        {
            Text += "Train winged\n";
        }
    }

    public void SubscribeToEvents()
    {
        EventHub.Subscribe<NewTrainInformationAvailable>(ProcessEvent);
        EventHub.Subscribe<TrainEvent>(ProcessEvent);
    }
}
