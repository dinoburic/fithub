using FitHub.Application.Modules.Blog.Comments.Commands.Create;
using FitHub.Application.Modules.Blog.Comments.Commands.Delete;
using FitHub.Application.Modules.Blog.Comments.Commands.Update;
using FitHub.Application.Modules.Blog.Comments.Queries.GetById;
using FitHub.Application.Modules.Blog.Comments.Queries.List;

namespace FitHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CommentsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateCommentCommand command, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetCommentByIdQueryDto>> GetById(int id, CancellationToken ct)
    {
        var comment = await sender.Send(new GetCommentByIdQuery { Id = id }, ct);
        return Ok(comment);
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<CommentListItemDto>>> List([FromQuery] ListCommentsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCommentCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteCommentCommand { Id = id }, ct);
        return NoContent();
    }
}
