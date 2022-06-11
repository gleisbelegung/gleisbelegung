using Gleisbelegung.App.Common;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Common;
using Godot;

public class EventLog : WindowDialog, IEventListener<NewTrainInformationAvailable>, IEventListener<TrainEvent>
{
    private Label _label;

    public EventLog()
    {
        this.RegisterSubscriptions();
    }

    public override void _Ready()
    {
        _label = GetNode<Label>("ScrollContainer/EventLog");
    }

    public void ProcessEvent(NewTrainInformationAvailable eventData)
    {
        // Text = Text + $"{eventData.Train.Name} arrived at FacilityName\n";
    }

    public void ProcessEvent(TrainEvent eventData)
    {
        if (eventData.Type == TrainEventType.Incoming)
        {
            // _label.Text += $"Train {eventData.Train.Name} arrived at facility\n";
        }
        else if (eventData.Type == TrainEventType.Arrival)
        {
            _label.Text += $"Train {eventData.Train.Name} arrived at a platform\n";
        }
        else if (eventData.Type == TrainEventType.Departure)
        {
            _label.Text += $"Train {eventData.Train.Name} ready to depart at a platform\n";
        }
        else if (eventData.Type == TrainEventType.RedSignal)
        {
            _label.Text += $"Train {eventData.Train.Name} has a red signal\n";
        }
        else if (eventData.Type == TrainEventType.BecameGreen)
        {
            _label.Text += $"Train {eventData.Train.Name} became green\n";
        }
        else if (eventData.Type == TrainEventType.Couple)
        {
            _label.Text += $"Train {eventData.Train.Name} coupled\n";
        }
        else if (eventData.Type == TrainEventType.Wing)
        {
            _label.Text += $"Train {eventData.Train.Name} winged\n";
        }
    }
}
