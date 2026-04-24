namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.GetById;

public sealed class GetBlogPostByIdQueryDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string? Content { get; init; }
    public int CategoryId { get; init; }
    public required string CategoryTitle { get; init; }
    public int UserId { get; init; }
    public required string AuthorName { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public int CommentsCount { get; init; }
}
