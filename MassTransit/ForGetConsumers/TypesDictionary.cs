using MassTransit;
using MassTransitShared.ForGetConsumers;
using System.Reflection;

namespace MassTransitShared.ForGetConsumers
{
    public class TypesDictionary
    {
        public Dictionary<Type, Type> Types { get; }
        public TypesDictionary(Assembly assembly )
        {
            Types = new Dictionary<Type, Type>();
            foreach (var consumer in TypeExt.GetConsumers(assembly))
                Types[consumer] = consumer.GetInterface(typeof(IConsumer<>).FullName).GetGenericArguments()[0];
        }
    }
}
