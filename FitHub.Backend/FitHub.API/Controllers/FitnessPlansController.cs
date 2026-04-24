using FitHub.Application.Modules.Training.FitnessPlans.Commands.Delete;
using FitHub.Application.Modules.Workout.FitnessPlans.Commands.Create;
using FitHub.Application.Modules.Workout.FitnessPlans.Commands.Update;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.GetById;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.Recommendations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FitnessPlansController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateFitnessPlan(CreateFitnessPlanCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);

            return Ok(new { id });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetFitnessPlanByIdQueryDto>> GetFitnessPlanById(int id, CancellationToken ct)
        {
            var query = new GetFitnessPlanByIdQuery { Id = id };
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PageResult<ListFitnessPlansQueryDto>>> GetFitnessPlans([FromQuery] ListFitnessPlansQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFitnessPlan(int id, UpdateFitnessPlanCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFitnessPlan(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteFitnessPlanCommand { Id = id }, ct);
            return NoContent();
        }

        [HttpGet("recommendations")]
        [Authorize] 
        public async Task<ActionResult<List<ListFitnessPlansQueryDto>>> GetRecommendations(CancellationToken ct)
        {
            var query = new GetRecommendedPlansQuery { Count = 4 };
            var result = await sender.Send(query, ct);

            return Ok(result);
        }
    }
}
