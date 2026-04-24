using FitHub.Application.Modules.Training.FitnessPlans.Commands.Delete;
using FitHub.Application.Modules.Users.Commands.Create;
using FitHub.Application.Modules.Users.Commands.Delete;
using FitHub.Application.Modules.Users.Commands.Update;
using FitHub.Application.Modules.Users.Queries.GetById;
using FitHub.Application.Modules.Users.Queries.List;
using FitHub.Application.Modules.Workout.FitnessPlans.Commands.Update;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.GetById;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateUser(CreateUserCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);

            return Ok(new { id });
       }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserByIdQuery>> GetUserById(int id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<ListUsersQueryDto>>> GetUsers([FromQuery] ListUsersQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteUserCommand { Id = id }, ct);
            return NoContent();
        }
    }
}
