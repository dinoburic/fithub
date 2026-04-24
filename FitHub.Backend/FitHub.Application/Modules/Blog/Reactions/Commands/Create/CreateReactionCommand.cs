namespace FitHub.Application.Modules.Blog.Reactions.Commands.Create;

public sealed class CreateReactionCommand : IRequest<int>
{
    public required int BlogPostId { get; set; }
    public required int UserId { get; set; }
    public required string Type { get; set; } // e.g., "like", "love", "insightful"
}
