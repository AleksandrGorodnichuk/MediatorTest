using MassTransit;
using MassTransit.Mediator;

namespace MassTransitShared
{
    public static class MediatorAdapter
    {
        //public static async Task<TResponse> send<TRequest, TResponse>(this IMediator mediator, TRequest request, IBus bus = null) where TRequest : class, IRequest<TResponse> where TResponse: class
        public static async Task<TResponse> send<TResponse>(this IMediator mediator, IRequest<TResponse> request, IBus bus = null) where TResponse : class
        {
            try 
            {
                var client = mediator.CreateRequest(request);
                var response = await client.GetResponse<TResponse>();
                return response.Message;
            }
            catch (RequestException ex)
            {
                var client = bus.CreateRequestClient<IRequest<TResponse>>();
                var demandsTask = await client.GetResponse<TResponse>(request);
                return demandsTask.Message;
            }
        }
    }
}
