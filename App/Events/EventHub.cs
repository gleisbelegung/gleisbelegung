using System;
using System.Collections.Generic;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Extensions;
using Godot;
// using PubSub;

namespace Gleisbelegung.App.Events
{
    public static class EventHub
    {
        public static void RegisterSubscriptions(object instance)
        {
            // var temp = (IEventListener<>)instance;
            var instanceType = instance.GetType();
            var types = ReflectionHelper.GetListOfGenericInterfaceTypes(instanceType, typeof(IEventListener<>));
            foreach (var t in types)
            {
                var methodInfo = instanceType.GetMethod("ProcessEvent", new[] { t });

                // GD.Print($"{DateTime.Now.ToLogTime()} EventHub.RegisterSubscribe: {t.Name}");
                Hub.Default.Subscribe(t, (data) =>
                {
                    // GD.Print($"{DateTime.Now.ToLogTime()} EventHub.Subscribe: {data.GetType().Name}");
                    methodInfo.Invoke(instance, new[] { data });
                });
            }
        }

        public static void Publish<T>(T eventData)
        {
            // GD.Print($"{DateTime.Now.ToLogTime()} EventHub.Publish: {eventData.GetType().Name}");
            Hub.Default.Publish<T>(eventData);
        }
    }
}