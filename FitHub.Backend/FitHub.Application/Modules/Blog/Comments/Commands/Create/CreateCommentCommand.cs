namespace FitHub.Application.Modules.Blog.Comments.Commands.Create;

public sealed class CreateCommentCommand : IRequest<int>
{
    public required int BlogPostId { get; set; }
    public required int UserId { get; set; }
    public required string Content { get; set; }
}
