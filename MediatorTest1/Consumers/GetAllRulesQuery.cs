using MassTransit;
using MassTransitShared;

namespace MediatorTest1.Consumers
{
    public class string2
    {
        public string core { get; set; }
    }
    public class GetAllRulesQuery : IRequest<string2>
    {
        public string number { get; set; }
    }

    public class GetAllRulesHandler : IConsumer<GetAllRulesQuery>
    {
       
        public async Task Consume(ConsumeContext<GetAllRulesQuery> context)
        {
            var query = context.Message;
            context.Respond(new string2() { core = query.number });
        }
    }
}
