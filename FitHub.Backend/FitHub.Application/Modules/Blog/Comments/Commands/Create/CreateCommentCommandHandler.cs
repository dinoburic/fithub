using FluentValidation;
using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using CommentEntity = FitHub.Domain.Entities.Blog.Comments;

namespace FitHub.Application.Modules.Blog.Comments.Commands.Create;

public sealed class CreateCommentCommandHandler(IAppDbContext ctx, TimeProvider clock)
    : IRequestHandler<CreateCommentCommand, int>
{
    public async Task<int> Handle(CreateCommentCommand request, CancellationToken ct)
    {
        var content = request.Content?.Trim();
        if (string.IsNullOrWhiteSpace(content))
            throw new ValidationException("Content is required.");

        bool blogPostExists = await ctx.BlogPosts
            .AsNoTracking()
            .AnyAsync(b => b.BlogPostID == request.BlogPostId && !b.IsDeleted, ct);
        if (!blogPostExists)
            throw new MarketNotFoundException($"Blog post (ID={request.BlogPostId}) doesn't exist.");

        bool userExists = await ctx.Users
            .AsNoTracking()
            .AnyAsync(u => u.UserID == request.UserId, ct);
        if (!userExists)
            throw new MarketNotFoundException($"User (ID={request.UserId}) doesn't exist.");

        var entity = new CommentEntity
        {
            CommentID = 0,
            BlogPostID = request.BlogPostId,
            UserID = request.UserId,
            Content = content,
            CreatedAtUTC = clock.GetUtcNow().UtcDateTime,
            BlogPost = null!,
            User = null!,
            IsDeleted = false
        };

        ctx.Comments.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.CommentID;
    }
}
