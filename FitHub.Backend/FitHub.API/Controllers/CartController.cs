using FitHub.Application.Modules.Cart.Commands.AddToCart;
using FitHub.Application.Modules.Cart.Commands.DeleteFromCart;
using FitHub.Application.Modules.Cart.Queries.GetMyCart;
using FitHub.Application.Modules.Cart.SaveForLater.MoveBackToCartCommand;
using FitHub.Application.Modules.Cart.SaveForLater.SaveForLaterCommand;
using MediatR; // Opcionalno, zavisno kako je ISender importovan u global usings
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> AddToCart(AddToCartCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);

            return Ok(new { id });
        }

        [HttpGet]
        public async Task<ActionResult<GetMyCartQueryDto>> GetMyCart([FromQuery] GetMyCartQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteFromCartCommand { CartItemID = id }, ct);
            return NoContent();
        }

        [HttpPut("save-for-later/{id}")]
        public async Task<IActionResult> SaveForLater(int id, CancellationToken ct)
        {
            await sender.Send(new SaveForLaterCommand { CartItemID = id }, ct);
            return NoContent();
        }

        [HttpPut("move-back-to-cart/{id}")]
        public async Task<IActionResult> MoveBackToCart(int id, CancellationToken ct)
        {
            await sender.Send(new MoveBackToCartCommand { CartItemID = id }, ct);
            return NoContent();
        }
    }
}