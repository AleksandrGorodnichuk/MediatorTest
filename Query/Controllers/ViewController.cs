using MassTransit;
using MassTransit.Mediator;
using MassTransitShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTS.Demands.CoreAPI.Application.Features.Rules.Quares;

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
        [HttpGet]
        public async Task<IActionResult> GetView()
        {
            //int num = 7;

            return Ok(await _mediator.send/*<GetAllRulesQuery, string2>*/(new GetAllRulesQuery() { number = "7" }, _bus));
        }
    }
}
