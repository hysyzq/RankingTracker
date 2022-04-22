using Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RankingTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingTrackController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Track(RankingTrackRequest request)
        {
            var response = new RankingTrackResponse
            {
                positions = new List<int>
                {
                    1, 10,33
                }
            };
            return Ok(response);
        }
    }
}
