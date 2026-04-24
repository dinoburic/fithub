using FitHub.Application.Modules.Orders.Commands.Create;
using FitHub.Application.Modules.Orders.Commands.Delete;
using FitHub.Application.Modules.Orders.Commands.Update;
using FitHub.Application.Modules.Orders.Queries.GetById;
using FitHub.Application.Modules.Orders.Queries.List;
using FitHub.Application.Modules.Training.FitnessPlans.Commands.Delete;
using FitHub.Application.Modules.Workout.FitnessPlans.Commands.Create;
using FitHub.Application.Modules.Workout.FitnessPlans.Commands.Update;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.GetById;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class OrdersController(ISender sender) : ControllerBase
        {
            [HttpPost]
            public async Task<ActionResult<int>> CreateOrder(CreateOrderCommand command, CancellationToken ct)
            {
                int id = await sender.Send(command, ct);

                return Ok(new { id });
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<GetOrderByIdQueryDto>> GetOrderById(int id, CancellationToken ct)
            {
                var query = new GetOrderByIdQuery { Id = id };
                var result = await sender.Send(query, ct);
                return Ok(result);
            }

            [HttpGet]
            public async Task<ActionResult<PageResult<ListOrdersQueryDto>>> GetOrders([FromQuery] ListOrdersQuery query, CancellationToken ct)
            {
                var result = await sender.Send(query, ct);
                return Ok(result);
            }


            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateOrder(int id, UpdateOrderCommand command, CancellationToken ct)
            {
                command.OrderID = id;
                await sender.Send(command, ct);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteOrder(int id, CancellationToken ct)
            {
                await sender.Send(new DeleteOrderCommand { OrderID = id }, ct);
                return NoContent();
            }

            
        }
    
}
