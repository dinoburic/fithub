using FitHub.Application.Modules.Wishlists.Commands.AddWishlistItem;
using FitHub.Application.Modules.Wishlists.Commands.Remove;
using FitHub.Application.Modules.Wishlists.Queries.GetMyWishlist;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PageResult<GetMyWishlistQueryDto>>> GetMyWishlist([FromQuery] GetMyWishlistQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpPost("{fitnessPlanId}")]
        public async Task<ActionResult<int>> AddToWishlist(int fitnessPlanId, CancellationToken ct)
        {
            var command = new AddWishListItemCommand { FitnessPlanID = fitnessPlanId };
            int id = await sender.Send(command, ct);

            return Ok(new { id });
        }

      
        [HttpDelete("{fitnessPlanId}")]
        public async Task<IActionResult> RemoveFromWishlist(int fitnessPlanId, CancellationToken ct)
        {
            var command = new RemoveWishListItemCommand { FitnessPlanID = fitnessPlanId };
            await sender.Send(command, ct);

            return NoContent();
        }
    }
}
