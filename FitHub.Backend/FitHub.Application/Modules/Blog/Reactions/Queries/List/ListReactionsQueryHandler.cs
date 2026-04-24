using FitHub.Application.Abstractions;
using FitHub.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.Reactions.Queries.List;

public sealed class ListReactionsQueryHandler(IAppDbContext ctx)
    : IRequestHandler<ListReactionsQuery, PageResult<ReactionListItemDto>>
{
    public async Task<PageResult<ReactionListItemDto>> Handle(ListReactionsQuery request, CancellationToken ct)
    {
        var query = ctx.Reactions
            .AsNoTracking()
            .Where(r => !r.IsDeleted)
            .OrderByDescending(r => r.DateTimeAddedUTC)
            .AsQueryable();

        if (request.BlogPostId.HasValue)
            query = query.Where(r => r.BlogPostID == request.BlogPostId.Value);

        if (request.UserId.HasValue)
            query = query.Where(r => r.UserID == request.UserId.Value);

        if (!string.IsNullOrWhiteSpace(request.Type))
            query = query.Where(r => r.Type == request.Type.Trim().ToLowerInvariant());

        var projection = query.Select(r => new ReactionListItemDto
        {
            Id = r.ReactionID,
            BlogPostId = r.BlogPostID,
            UserId = r.UserID,
            UserName = r.User != null ? r.User.Name + " " + r.User.Surname : string.Empty,
            Type = r.Type,
            DateTimeAddedUtc = r.DateTimeAddedUTC
        });

        return await PageResult<ReactionListItemDto>.FromQueryableAsync(projection, request.Paging, ct);
    }
}
