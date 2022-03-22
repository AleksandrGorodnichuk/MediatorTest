using MassTransit.Util;
using System.Reflection;
using MassTransit;
using MassTransit.Internals;

namespace MassTransitShared.ForGetConsumers
{
    public static class TypeExt
    {
        public static bool IsOpenGeneric(this TypeInfo typeInfo)
        {
            return typeInfo.IsGenericTypeDefinition || typeInfo.ContainsGenericParameters;
        }
        public static Type[] GetConsumers( Func<Type, bool> filter, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types1 = AssemblyTypeCache.FindTypes(assemblies, IsConsumerOrDefinition).GetAwaiter().GetResult();
            var types = types1.FindTypes(TypeClassification.Concrete | TypeClassification.Closed).ToArray();
            IEnumerable<Type> consumerTypes = types.Where(MessageTypeCache.HasConsumerInterfaces);
            IEnumerable<Type> consumerDefinitionTypes = types.Where(x => x.HasInterface(typeof(IConsumerDefinition<>)));

            var consumers = from c in consumerTypes
                            join d in consumerDefinitionTypes on c equals d.GetClosingArgument(typeof(IConsumerDefinition<>)) into dc
                            from d in dc.DefaultIfEmpty()
                            where filter(c)
                            select new
                            {
                                ConsumerType = c,
                                DefinitionType = d
                            };

            return default; //types.FindTypes(TypeClassification.Concrete | TypeClassification.Closed).ToArray();            
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
