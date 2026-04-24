using FitHub.Application.Modules.Blog.BlogPosts.Commands.Create;
using FitHub.Application.Modules.Blog.BlogPosts.Commands.Delete;
using FitHub.Application.Modules.Blog.BlogPosts.Commands.Update;
using FitHub.Application.Modules.Blog.BlogPosts.Queries.GetById;
using FitHub.Application.Modules.Blog.BlogPosts.Queries.List;
using Microsoft.AspNetCore.Authorization;

namespace FitHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BlogPostsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateBlogPostCommand command, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetBlogPostByIdQueryDto>> GetById(int id, CancellationToken ct)
    {
        var post = await sender.Send(new GetBlogPostByIdQuery { Id = id }, ct);
        return Ok(post);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<PageResult<BlogPostListItemDto>>> List([FromQuery] ListBlogPostsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateBlogPostCommand command, CancellationToken ct)
    {
        command.Id = id;
        await sender.Send(command, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteBlogPostCommand { Id = id }, ct);
        return NoContent();
    }
}
