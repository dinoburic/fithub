using FitHub.Application.Modules.Reviews.Commands.Create;
using FitHub.Application.Modules.Reviews.Commands.Delete;
using FitHub.Application.Modules.Reviews.Commands.Update;
using FitHub.Application.Modules.Reviews.Queries.GetById;
using FitHub.Application.Modules.Reviews.Queries.GetByPlanId;
using FitHub.Application.Modules.Reviews.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateReview(CreateReviewCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return Ok(new { id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetReviewByIdQueryDto>> GetReviewById(int id, CancellationToken ct)
        {
            var query = new GetReviewByIdQuery { Id = id };
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<ListReviewsQueryDto>>> GetReviews([FromQuery] ListReviewsQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewCommand command, CancellationToken ct)
        {
            command.ReviewID = id;
            await sender.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteReviewCommand { ReviewID = id }, ct);
            return NoContent();
        }

        [HttpGet("plan/{fitnessPlanId}")]
        public async Task<ActionResult<List<GetReviewsByPlanIdQueryDto>>> GetReviewsByPlanId(int fitnessPlanId, CancellationToken ct)
        {
            var query = new GetReviewsByPlanIdQuery { FitnessPlanID = fitnessPlanId };
            var result = await sender.Send(query, ct);
            return Ok(result);
        }
    }
}
