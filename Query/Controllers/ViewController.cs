using MassTransit;
using MassTransit.Mediator;
using MassTransitShared;
using MediatorTest1.Consumers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private IMediator _mediator;
        private IBus _bus;
        public ViewController(IMediator mediator, IBus bus)
        {
            _mediator = mediator;
            _bus = bus;
        }
        [HttpGet("GetView")]
        public async Task<IActionResult> GetView()
        {
            return Ok(await MediatorAdapter.send<GetAllRulesQuery, string2>(null, new GetAllRulesQuery() { number = "7" }, Assembly.GetExecutingAssembly(), _bus));
        }
        [HttpGet("GetViewLocal")]
        public async Task<IActionResult> GetViewLocal()
        {
            return Ok(await _mediator.send<GetQuery, string3>(new GetQuery() { number = "5" }, Assembly.GetExecutingAssembly(), _bus));
        }
    }
}
