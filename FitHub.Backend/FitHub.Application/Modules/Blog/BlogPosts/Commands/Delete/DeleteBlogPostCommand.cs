namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Delete;

public sealed class DeleteBlogPostCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
