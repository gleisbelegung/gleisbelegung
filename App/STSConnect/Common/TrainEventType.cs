using System.Collections.Generic;

namespace Gleisbelegung.App.STSConnect.Common
{
    public class TrainEventType
    {
        public static TrainEventType Incoming = new TrainEventType("einfahrt");
        public static TrainEventType Arrival = new TrainEventType("ankunft");
        public static TrainEventType Departure = new TrainEventType("abfahrt");
        public static TrainEventType Leaving = new TrainEventType("ausfahrt");
        public static TrainEventType RedSignal = new TrainEventType("rothalt");
        public static TrainEventType BecameGreen = new TrainEventType("wurdegruen");
        public static TrainEventType Couple = new TrainEventType("kuppeln");
        public static TrainEventType Wing = new TrainEventType("fluegeln");

        public static List<TrainEventType> AllTrainEvents = new List<TrainEventType> {
            Incoming,
            Arrival,
            Departure,
            Leaving,
            RedSignal,
            BecameGreen,
            Couple,
            Wing
        };

        public string EventName { get; }

        public TrainEventType(string eventName)
        {
            EventName = eventName;
        }
    }
}