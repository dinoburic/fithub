using FitHub.Application.Modules.Blog.Reactions.Commands.Create;
using FitHub.Application.Modules.Blog.Reactions.Commands.Delete;
using FitHub.Application.Modules.Blog.Reactions.Queries.GetById;
using FitHub.Application.Modules.Blog.Reactions.Queries.List;

namespace FitHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ReactionsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateReactionCommand command, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetReactionByIdQueryDto>> GetById(int id, CancellationToken ct)
    {
        var reaction = await sender.Send(new GetReactionByIdQuery { Id = id }, ct);
        return Ok(reaction);
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<ReactionListItemDto>>> List([FromQuery] ListReactionsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteReactionCommand { Id = id }, ct);
        return NoContent();
    }

    /// <summary>
    /// Get reaction counts grouped by type for a specific blog post
    /// </summary>
    [HttpGet("summary/{blogPostId:int}")]
    public async Task<ActionResult<object>> GetSummary(int blogPostId, CancellationToken ct)
    {
        var query = new ListReactionsQuery { BlogPostId = blogPostId, Paging = new PageRequest { PageSize = 1000 } };
        var result = await sender.Send(query, ct);
        
        var summary = result.Items
            .GroupBy(r => r.Type)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToDictionary(x => x.Type, x => x.Count);

        return Ok(new 
        { 
            BlogPostId = blogPostId,
            TotalReactions = result.Total,
            ByType = summary
        });
    }
}
