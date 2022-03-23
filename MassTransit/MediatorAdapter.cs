using MassTransit;
using System;
using MassTransit.Mediator;
using MassTransitShared.ForGetConsumers;
using System.Reflection;

namespace MassTransitShared
{
    public static class MediatorAdapter
    {
        public static async Task<TResponse> send<TRequest, TResponse>(this IMediator mediator, TRequest request, Assembly assembly, IBus bus = null) where TRequest : class, IRequest<TResponse> where TResponse: class
        //public static async Task<TResponse> send<TResponse>(this IMediator mediator, IRequest<TResponse> request, IBus bus = null) where TResponse : class
        {
            /*var consumers = TypeExt.GetConsumers(null, assembly);
            List<Type> querys = new List<Type>();
            foreach (var consumer in consumers)
            {
               querys.Add(consumer.GetInterface(nameof(IConsumer)+"`1").GetGenericArguments()[0]);
            }
            if (querys.Contains(typeof(TRequest)))*/
            /*{ */
                var hendler = mediator.CreateRequestClient<TRequest>();
                var response = await hendler.GetResponse<TResponse>(request);
                return response.Message;
            /*}
            else 
            {
                var client = bus.CreateRequestClient<TRequest>();
                var demandsTask = await client.GetResponse<TResponse>(request);
                return demandsTask.Message;
            }*/
            
        }
    }
}
