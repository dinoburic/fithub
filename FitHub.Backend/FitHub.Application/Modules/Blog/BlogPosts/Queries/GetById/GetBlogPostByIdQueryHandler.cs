using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.GetById;

public sealed class GetBlogPostByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetBlogPostByIdQuery, GetBlogPostByIdQueryDto>
{
    public async Task<GetBlogPostByIdQueryDto> Handle(GetBlogPostByIdQuery request, CancellationToken ct)
    {
        var post = await ctx.BlogPosts
            .AsNoTracking()
            .Where(b => b.BlogPostID == request.Id)
            .Select(b => new GetBlogPostByIdQueryDto
            {
                Id = b.BlogPostID,
                Title = b.Title,
                Content = b.Content,
                CategoryId = b.CategoryID,
                CategoryTitle = b.Category != null ? b.Category.Title : string.Empty,
                UserId = b.UserID,
                AuthorName = b.User != null ? b.User.Name + " " + b.User.Surname : string.Empty,
                CreatedAtUtc = b.CreatedAtUTC,
                UpdatedAtUtc = b.UpdatedAtUTC,
                CommentsCount = b.Comments.Count
            })
            .FirstOrDefaultAsync(ct);

        if (post is null)
            throw new MarketNotFoundException($"Blog post (ID={request.Id}) doesn't exist.");

        return post;
    }
}
