using System;
using Gleisbelegung.App.Extensions;
using Godot;
using PubSub;

namespace Gleisbelegung.App.Events
{
    public static class EventHub
    {
        public static void Subscribe<T>(Action<T> handler)
        {
            GD.Print($"{DateTime.Now.ToLogTime()} EventHub.RegisterSubscribe: {typeof(T).Name}");
            Hub.Default.Subscribe<T>((eventData) =>
            {
                GD.Print($"{DateTime.Now.ToLogTime()} EventHub.Subscribe: {eventData.GetType().Name}");
                handler(eventData);
            });
        }

        public static void Publish<T>(T eventData)
        {
            GD.Print($"{DateTime.Now.ToLogTime()} EventHub.Publish: {eventData.GetType().Name}");
            Hub.Default.Publish<T>(eventData);
        }
    }
}