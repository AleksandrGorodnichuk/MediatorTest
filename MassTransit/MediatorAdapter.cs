using MassTransit;
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
        public async Task<TResponse> send<TResponse>(IRequest<TResponse> request)  where TResponse: class
        {
            if (_typesDictionary.Types.ContainsValue(request.GetType()))
            {
                var handler = _mediator.CreateRequest(request);
                var response = await handler.GetResponse<TResponse>();
                return response.Message;

            }
            else
            {
                MethodInfo createRequestClient = typeof(ClientFactoryExtensions).GetMethod(nameof(ClientFactoryExtensions.CreateRequestClient), new Type[] { typeof(IBus), typeof(RequestTimeout) }).MakeGenericMethod(request.GetType());
                dynamic client = createRequestClient.Invoke(null, new object[] { _bus, default(RequestTimeout) });
                Response<TResponse> response = await client.GetResponse<TResponse>(request, default(CancellationToken), default(RequestTimeout));
                return response.Message; 
            }
            
        }
    }
}
