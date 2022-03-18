using MassTransit;
using MassTransitShared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Query
{
    public class string3
    {
        public string core { get; set; }
    }
    public class GetQuery : IRequest<string3>
    {
        public string number { get; set; }
    }

    public class GetHandler : IConsumer<GetQuery>
    {

        public async Task Consume(ConsumeContext<GetQuery> context)
        {
            var query = context.Message;
            context.Respond(new string3() { core = query.number });
        }
    }
}
