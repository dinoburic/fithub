namespace FitHub.Application.Modules.Blog.Comments.Commands.Update;

public sealed class UpdateCommentCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required string Content { get; set; }
}
