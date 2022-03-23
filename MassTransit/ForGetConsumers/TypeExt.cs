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
        public static bool IsOpenGeneric(this TypeInfo typeInfo)
        {
            return typeInfo.IsGenericTypeDefinition || typeInfo.ContainsGenericParameters;
        }
        public static IEnumerable<Type> GetConsumers( Func<Type, bool> filter, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types1 = AssemblyTypeCache.FindTypes(assemblies, IsConsumerOrDefinition).GetAwaiter().GetResult();
            var types = types1.FindTypes(TypeClassification.Concrete | TypeClassification.Closed).ToArray();
            IEnumerable<Type> consumerTypes = types.Where(TypeMetadataCache.HasConsumerInterfaces);
            /*IEnumerable<Type> consumerDefinitionTypes = types.Where(x => x.HasInterface(typeof(IConsumerDefinition<>)));

            var consumers = from c in consumerTypes
                            join d in consumerDefinitionTypes on c equals d.GetClosingArgument(typeof(IConsumerDefinition<>)) into dc
                            from d in dc.DefaultIfEmpty()
                            //where filter(c)
                            select new
                            {
                                ConsumerType = c,
                                DefinitionType = d
                            };
            foreach (var consumer in consumers)
            {
                var a = consumer.ConsumerType;
                var b = consumer.DefinitionType;
            }*/
            return consumerTypes;            
        }

        public static bool IsConsumerOrDefinition(Type type)
        {
            Type[] interfaces = type.GetTypeInfo().GetInterfaces();

            return interfaces.Any(t => InterfaceExtensions.HasInterface(t, typeof(MassTransit.IConsumer<>))
                || InterfaceExtensions.HasInterface(t, typeof(IJobConsumer<>))
                || InterfaceExtensions.HasInterface(t, typeof(IConsumerDefinition<>)));
        }
    }
}
