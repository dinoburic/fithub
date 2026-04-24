namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Create;

public sealed class CreateBlogPostCommand : IRequest<int>
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required int CategoryId { get; set; }
    public required int UserId { get; set; }
}
