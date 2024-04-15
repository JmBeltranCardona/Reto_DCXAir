using Application.Cqrs.Journey.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRoute([FromQuery] GetRouteQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
