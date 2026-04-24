using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.Reactions.Queries.GetById;

public sealed class GetReactionByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetReactionByIdQuery, GetReactionByIdQueryDto>
{
    public async Task<GetReactionByIdQueryDto> Handle(GetReactionByIdQuery request, CancellationToken ct)
    {
        var reaction = await ctx.Reactions
            .AsNoTracking()
            .Where(r => r.ReactionID == request.Id && !r.IsDeleted)
            .Select(r => new GetReactionByIdQueryDto
            {
                Id = r.ReactionID,
                BlogPostId = r.BlogPostID,
                BlogPostTitle = r.BlogPost != null ? r.BlogPost.Title : string.Empty,
                UserId = r.UserID,
                UserName = r.User != null ? r.User.Name + " " + r.User.Surname : string.Empty,
                Type = r.Type,
                DateTimeAddedUtc = r.DateTimeAddedUTC
            })
            .FirstOrDefaultAsync(ct);

        if (reaction is null)
            throw new MarketNotFoundException($"Reaction (ID={request.Id}) doesn't exist.");

        return reaction;
    }
}
