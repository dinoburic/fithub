namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.List;

public sealed class BlogPostListItemDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string? ContentPreview { get; init; }
    public int CategoryId { get; init; }
    public required string CategoryTitle { get; init; }
    public int UserId { get; init; }
    public required string AuthorName { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
