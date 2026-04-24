namespace FitHub.Application.Modules.Blog.Comments.Queries.List;

public sealed class CommentListItemDto
{
    public int Id { get; init; }
    public int BlogPostId { get; init; }
    public int UserId { get; init; }
    public required string AuthorName { get; init; }
    public required string Content { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
