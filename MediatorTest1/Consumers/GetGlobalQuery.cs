using MassTransit;
using MassTransitShared;

namespace MediatorTest.Consumers
{
    public class string2
    {
        public string core { get; set; }
    }
    public class GetGlobalQuery : IRequest<string2>
    {
        public string number { get; set; }
    }

    public class GetGlobalHandler : IConsumer<GetGlobalQuery>
    {   
        public async Task Consume(ConsumeContext<GetGlobalQuery> context)
        {
            var query = context.Message;
            context.Respond(new string2() { core = query.number + Random.Shared.NextDouble().ToString() });
        }
    }
}
