using System;
using System.Collections.Generic;

namespace ConsoleApp.MethodCalls
{
    internal class SampleGeneric<T>
    {
        public long Process(T obj)
        {
            return string.Format("{0} [{1}]", obj.ToString(), obj.GetType().FullName).Length;
        }
    }

    internal class Container
    {
        private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();

        public static void Register<T>(SampleGeneric<T> instance)
        {
            if (false == Instances.ContainsKey(typeof(T)))
            {
                Instances.Add(typeof(T), instance);
            }
            else
            {
                Instances[typeof(T)] = instance;
            }
        }

        public static SampleGeneric<T> Get<T>()
        {
            if (false == Instances.ContainsKey(typeof(T))) throw new KeyNotFoundException();
            return (SampleGeneric<T>)Instances[typeof(T)];
        }

        public static object Get(Type type)
        {
            if (false == Instances.ContainsKey(type)) throw new KeyNotFoundException();
            return Instances[type];
        }
    }
}