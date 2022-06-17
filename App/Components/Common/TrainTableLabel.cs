using System;
using System.Security.Cryptography;
using System.Text;
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
                var hashingContext = new HashingContext();
                hashingContext.Start(HashingContext.HashType.Md5);
                hashingContext.Update(Encoding.UTF8.GetBytes(Text));
                var hash = hashingContext.Finish();

                var r = Convert.ToInt32(hash[0]);
                var g = Convert.ToInt32(hash[1]);
                var b = Convert.ToInt32(hash[2]);

                var styleBox = (StyleBoxFlat)GetStylebox("normal").Duplicate();
                styleBox.BgColor = new Color(r / (float)255, g / (float)255, b / (float)255, 1);
                AddStyleboxOverride("normal", styleBox);
            }
        }
    }
}