using MassTransit;
using MassTransitShared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Query.Consumers
{
    public class GetHandler : IConsumer<GetQuery>
    {
        public async Task Consume(ConsumeContext<GetQuery> context)
        {
            var query = context.Message;
            context.Respond(new string3() { core = query.number + Random.Shared.NextDouble().ToString() });
        }
    }
}
