using System;
using Fernandezja.ColorHashSharp;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.Extensions;
using Godot;

namespace Gleisbelegung.App.Components.Common
{
    public class TrainTableLabel : TableLabel, IEventListener<TrainScheduleChangedEvent>
    {
        private readonly Platform _platform;
        private readonly TimeSpan _time;

        public TrainTableLabel(Platform platform, TimeSpan time) : base("")
        {
            this.RegisterSubscriptions();

            _platform = platform;
            _time = time;
        }

        public void ProcessEvent(TrainScheduleChangedEvent eventData)
        {

            if (eventData.TrainSchedule.ActualPlatform != _platform)
            {
                return;
            }

            var departure = eventData.Train.GetActualDeparture(eventData.TrainSchedule);
            var arrival = eventData.Train.GetActualArrival(eventData.TrainSchedule);

            if (departure == null || arrival == null)
            {
                GD.Print($"TrainTableLabel: {eventData.Train.Name} departure or arrival is null");
                return;
            }

            if (_time >= arrival && _time <= departure)
            {
                Text += $"{eventData.Train.Name}{eventData.Train.GetDelayAsString()}";
            }

            if (!string.IsNullOrEmpty(Text))
            {
                var colorHash = new ColorHash();
                var color = colorHash.Rgb(Text);

                var styleBox = (StyleBoxFlat)GetStylebox("normal").Duplicate();
                styleBox.BgColor = new Color(color.R / (float)255, color.G / (float)255, color.B / (float)255, 1);
                AddStyleboxOverride("normal", styleBox);
            }
        }
    }
}