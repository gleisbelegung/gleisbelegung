
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gleisbelegung.App.Common
{
    public class ReflectionHelper
    {
        public static List<Type> GetListOfGenericInterfaceTypes(Type type, Type genericType)
        {
            var types = new List<Type>();
            var interfaceTypes = type.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    types.AddRange(it.GetGenericArguments());
            }

            return types;
        }

        public static List<T> CreateInstancesByInterface<T>()
        {
            var interfaceType = typeof(T);
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(x => (T)Activator.CreateInstance(x))
                    .ToList();
        }
    }
}