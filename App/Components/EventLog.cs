using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Godot;

public class EventLog : Label, IEventListener<NewTrainInformationAvailable>, IEventListener<TrainEvent>
{
    public EventLog()
    {
        this.RegisterSubscriptions();
    }

    public void ProcessEvent(NewTrainInformationAvailable eventData)
    {
        // Text = Text + $"{eventData.Train.Name} arrived at FacilityName\n";
    }

    public void ProcessEvent(TrainEvent eventData)
    {
        if (eventData.Type == TrainEventType.Incoming)
        {
            // Text += $"Train {eventData.Train.Name} arrived at facility\n";
        }
        else if (eventData.Type == TrainEventType.Arrival)
        {
            Text += $"Train {eventData.Train.Name} arrived at a platform\n";
        }
        else if (eventData.Type == TrainEventType.Departure)
        {
            // Text += $"Train {eventData.Train.Name} ready to depart at a platform\n";
        }
        else if (eventData.Type == TrainEventType.RedSignal)
        {
            Text += $"Train {eventData.Train.Name} has a red signal\n";
        }
        else if (eventData.Type == TrainEventType.BecameGreen)
        {
            Text += $"Train {eventData.Train.Name} became green\n";
        }
        else if (eventData.Type == TrainEventType.Couple)
        {
            Text += $"Train {eventData.Train.Name} coupled\n";
        }
        else if (eventData.Type == TrainEventType.Wing)
        {
            Text += $"Train {eventData.Train.Name} winged\n";
        }
    }
}
