namespace FitHub.Application.Modules.Blog.Reactions.Commands.Delete;

public sealed class DeleteReactionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
