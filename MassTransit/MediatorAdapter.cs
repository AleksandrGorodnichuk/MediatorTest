using MassTransit;
using MassTransit.Clients;
using MassTransit.Mediator;
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
            IRequestClient<IRequest<TResponse>> client = default;
            if (_typesDictionary.Types.Values.Contains(request.GetType()))
            {
                Type type = typeof(IClientFactory);
                var ss = type.GetMethods();
                MethodInfo createClient = ss[9];
                MethodInfo createClientGen = createClient.MakeGenericMethod(request.GetType());
                var client2 = (createClientGen.Invoke(_mediator, new object[] { default(RequestTimeout) }));
                bool bool12 = IRequestClient<string> is IRequestClient<object>; = true
                IRequestClient<string> : RequestClient<object>
                    bool bool21 = string is object; RequestClient<object>= true
                //var client3 = (RequestClient<object>)client2;
                //Type sssss = client2.GetType(); 
            }
            else
            {
                MethodInfo createClient = typeof(ClientFactoryExtensions).GetMethod(nameof(ClientFactoryExtensions.CreateRequestClient)).MakeGenericMethod(request.GetType());
                client = createClient.Invoke(_bus, null) as IRequestClient<IRequest<TResponse>>;
            }
            var response = await client.GetResponse<TResponse>(request);
            return response.Message;
        }
    }
}
