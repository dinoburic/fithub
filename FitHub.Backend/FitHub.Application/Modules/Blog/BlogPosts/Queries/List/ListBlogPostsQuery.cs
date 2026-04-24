using FitHub.Application.Common;

namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.List;

public sealed class ListBlogPostsQuery : BasePagedQuery<BlogPostListItemDto>
{
    public string? Search { get; init; }
    public int? CategoryId { get; init; }
    public int? UserId { get; init; }
}
