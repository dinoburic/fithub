using FitHub.Application.Abstractions;
using FitHub.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.List;

public sealed class ListBlogPostsQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListBlogPostsQuery, PageResult<BlogPostListItemDto>>
{
    public async Task<PageResult<BlogPostListItemDto>> Handle(ListBlogPostsQuery request, CancellationToken ct)
    {
        var query = ctx.BlogPosts
            .AsNoTracking()
            .OrderByDescending(b => b.CreatedAtUTC)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim();
            query = query.Where(b =>
                b.Title.Contains(term) ||
                (b.Content != null && b.Content.Contains(term)));
        }

        if (request.CategoryId.HasValue)
            query = query.Where(b => b.CategoryID == request.CategoryId.Value);

        if (request.UserId.HasValue)
            query = query.Where(b => b.UserID == request.UserId.Value);

        var projection = query.Select(b => new BlogPostListItemDto
        {
            Id = b.BlogPostID,
            Title = b.Title,
            ContentPreview = b.Content != null && b.Content.Length > 200
                ? b.Content.Substring(0, 200) + "..."
                : b.Content,
            CategoryId = b.CategoryID,
            CategoryTitle = b.Category != null ? b.Category.Title : string.Empty,
            UserId = b.UserID,
            AuthorName = b.User != null ? b.User.Name + " " + b.User.Surname : string.Empty,
            CreatedAtUtc = b.CreatedAtUTC
        });

        return await PageResult<BlogPostListItemDto>.FromQueryableAsync(projection, request.Paging, ct);
    }
}
