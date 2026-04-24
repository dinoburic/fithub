using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.Comments.Queries.GetById;

public sealed class GetCommentByIdQueryHandler(IAppDbContext ctx)
    : IRequestHandler<GetCommentByIdQuery, GetCommentByIdQueryDto>
{
    public async Task<GetCommentByIdQueryDto> Handle(GetCommentByIdQuery request, CancellationToken ct)
    {
        var comment = await ctx.Comments
            .AsNoTracking()
            .Where(c => c.CommentID == request.Id && !c.IsDeleted)
            .Select(c => new GetCommentByIdQueryDto
            {
                Id = c.CommentID,
                BlogPostId = c.BlogPostID,
                BlogPostTitle = c.BlogPost != null ? c.BlogPost.Title : string.Empty,
                UserId = c.UserID,
                AuthorName = c.User != null ? c.User.Name + " " + c.User.Surname : string.Empty,
                Content = c.Content,
                CreatedAtUtc = c.CreatedAtUTC
            })
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            throw new MarketNotFoundException($"Comment (ID={request.Id}) doesn't exist.");

        return comment;
    }
}
