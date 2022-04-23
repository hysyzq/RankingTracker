using Contract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RankingTracker.Services.RankingTrackServices.Queries;

namespace RankingTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingTrackController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RankingTrackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Track(GetRankingQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
