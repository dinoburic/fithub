using FitHub.Application.Abstractions;
using FitHub.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.Comments.Queries.List;

public sealed class ListCommentsQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListCommentsQuery, PageResult<CommentListItemDto>>
{
    public async Task<PageResult<CommentListItemDto>> Handle(ListCommentsQuery request, CancellationToken ct)
    {
        var query = ctx.Comments
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .OrderByDescending(c => c.CreatedAtUTC)
            .AsQueryable();

        if (request.BlogPostId.HasValue)
            query = query.Where(c => c.BlogPostID == request.BlogPostId.Value);

        if (request.UserId.HasValue)
            query = query.Where(c => c.UserID == request.UserId.Value);

        var projection = query.Select(c => new CommentListItemDto
        {
            Id = c.CommentID,
            BlogPostId = c.BlogPostID,
            UserId = c.UserID,
            AuthorName = c.User != null ? c.User.Name + " " + c.User.Surname : string.Empty,
            Content = c.Content,
            CreatedAtUtc = c.CreatedAtUTC
        });

        return await PageResult<CommentListItemDto>.FromQueryableAsync(projection, request.Paging, ct);
    }
}
