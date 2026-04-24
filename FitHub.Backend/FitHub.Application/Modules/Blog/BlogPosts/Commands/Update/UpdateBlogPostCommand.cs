namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Update;

public sealed class UpdateBlogPostCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required int CategoryId { get; set; }
}
