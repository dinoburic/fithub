using FitHub.Application.Modules.Workout.Exercises.Commands.Create;
using FitHub.Application.Modules.Workout.Exercises.Commands.Delete;
using FitHub.Application.Modules.Workout.Exercises.Commands.Update;
using FitHub.Application.Modules.Workout.Exercises.Queries.GetById;
using FitHub.Application.Modules.Workout.Exercises.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateExercise(CreateExerciseCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);

            return Ok(new { id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetExerciseByIdQueryDto>> GetExerciseById(int id, CancellationToken ct)
        {
            var query = new GetExerciseByIdQuery { Id = id };
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ExerciseDto>>> GetExercises([FromQuery] GetExercisesQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, UpdateExerciseCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteExerciseCommand { Id = id }, ct);
            return NoContent();
        }
    }
}