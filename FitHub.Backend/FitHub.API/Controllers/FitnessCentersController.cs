using FitHub.Application.Modules.FitnessCenters.Queries.List;
using FitHub.Application.Modules.Users.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FitnessCentersController(ISender sender) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PageResult<ListFitnessCentersQueryDto>>> GetFitnessCenters([FromQuery] ListFitnessCentersQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }
    }
}
