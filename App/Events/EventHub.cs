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
            var instanceType = instance.GetType();
            var types = ReflectionHelper.GetListOfGenericInterfaceTypes(instanceType, typeof(IEventListener<>));
            foreach (var t in types)
            {
                var methodInfo = instanceType.GetMethod("ProcessEvent", new[] { t });

                Hub.Default.Subscribe(t, (data) =>
                {
                    methodInfo.Invoke(instance, new[] { data });
                });
            }
        }

        public static void Publish<T>(T eventData)
        {
            Hub.Default.Publish<T>(eventData);
        }
    }
}