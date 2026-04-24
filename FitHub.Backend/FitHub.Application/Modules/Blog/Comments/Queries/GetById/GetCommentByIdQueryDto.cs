namespace FitHub.Application.Modules.Blog.Comments.Queries.GetById;

public sealed class GetCommentByIdQueryDto
{
    public int Id { get; init; }
    public int BlogPostId { get; init; }
    public required string BlogPostTitle { get; init; }
    public int UserId { get; init; }
    public required string AuthorName { get; init; }
    public required string Content { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
