namespace FitHub.Application.Modules.Blog.Comments.Commands.Delete;

public sealed class DeleteCommentCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
