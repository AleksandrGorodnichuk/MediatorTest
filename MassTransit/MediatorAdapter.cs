using MassTransit;
using System;
using MassTransit.Mediator;
using MassTransitShared.ForGetConsumers;
using System.Reflection;

namespace MassTransitShared
{
    public class MediatorAdapter
    {
        private readonly IMediator _mediator;
        private readonly IBus _bus;
        private readonly TypesDictionary _typesDictionary;
        public MediatorAdapter(IMediator mediator, IBus bus, TypesDictionary typesDictionary)
        {
            _mediator = mediator;
            _bus = bus;
            _typesDictionary = typesDictionary;
        }        
        public async Task<TResponse> send<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse: class
        {

            var client = _typesDictionary.Types.Values.Contains(typeof(TRequest)) ? _mediator.CreateRequestClient<TRequest>() : _bus.CreateRequestClient<TRequest>();
            var response = await client.GetResponse<TResponse>(request);
            return response.Message;
        }
    }
}
