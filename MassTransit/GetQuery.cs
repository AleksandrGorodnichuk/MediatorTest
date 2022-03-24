using MassTransit;
using MassTransitShared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransitShared
{
    public class string3
    {
        public string core { get; set; }
    }
    public class GetQuery : IRequest<string3>
    {
        public string number { get; set; }
    }
}
