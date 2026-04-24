namespace FitHub.Application.Modules.Blog.Reactions.Queries.GetById;

public sealed class GetReactionByIdQueryDto
{
    public int Id { get; init; }
    public int BlogPostId { get; init; }
    public required string BlogPostTitle { get; init; }
    public int UserId { get; init; }
    public required string UserName { get; init; }
    public required string Type { get; init; }
    public DateTime DateTimeAddedUtc { get; init; }
}
