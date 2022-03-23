using MassTransit;
using MassTransit.Mediator;
using MassTransitShared;
using MediatorTest.Consumers;
using Microsoft.AspNetCore.Mvc;
using Query.Consumers;
using System.Reflection;

namespace Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : MediatorAdapterBase
    {
        public ViewController(MediatorAdapter mediator) : base(mediator) { }
        [HttpGet("GetView")]
        public async Task<IActionResult> GetView()
        {
            return Ok(await MediatorAdapter.send(new GetGlobalQuery() { number = "7" }));
        }
        [HttpGet("GetViewLocal")]
        public async Task<IActionResult> GetViewLocal()
        {
            return Ok(await MediatorAdapter.send(new GetQuery() { number = "5" }));
        }
    }
}
