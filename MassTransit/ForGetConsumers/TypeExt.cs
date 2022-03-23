using MassTransit.Util;
using System.Reflection;
using MassTransit;
using MassTransit.Internals; //8.0.0
using MassTransit.JobService; //7.3.1
using MassTransit.Definition; //7.3.1
using MassTransit.Internals.Extensions; //7.3.1
using MassTransit.Metadata; //7.3.1

namespace MassTransitShared.ForGetConsumers
{
    public static class TypeExt
    {
        public static IEnumerable<Type> GetConsumers(params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = AssemblyTypeCache.FindTypes(assemblies, IsConsumerOrDefinition).GetAwaiter().GetResult();
            var filteredTypes = types.FindTypes(TypeClassification.Concrete | TypeClassification.Closed).ToArray();
            IEnumerable<Type> consumerTypes = filteredTypes.Where(TypeMetadataCache.HasConsumerInterfaces);
            return consumerTypes;            
        }
        static bool IsConsumerOrDefinition(Type type)
        {
            Type[] interfaces = type.GetTypeInfo().GetInterfaces();
            return interfaces.Any(t => InterfaceExtensions.HasInterface(t, typeof(MassTransit.IConsumer<>)));
        }
    }
}
