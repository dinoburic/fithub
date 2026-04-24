using FluentValidation;
using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using ReactionEntity = FitHub.Domain.Entities.Blog.Reactions;

namespace FitHub.Application.Modules.Blog.Reactions.Commands.Create;

public sealed class CreateReactionCommandHandler(IAppDbContext ctx, TimeProvider clock)
    : IRequestHandler<CreateReactionCommand, int>
{
    private static readonly string[] ValidReactionTypes = { "like", "love", "insightful", "celebrate" };

    public async Task<int> Handle(CreateReactionCommand request, CancellationToken ct)
    {
        var type = request.Type?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(type) || !ValidReactionTypes.Contains(type))
            throw new ValidationException($"Invalid reaction type. Valid types: {string.Join(", ", ValidReactionTypes)}");

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

        // Check if user already reacted to this post
        var existingReaction = await ctx.Reactions
            .FirstOrDefaultAsync(r => r.BlogPostID == request.BlogPostId 
                                      && r.UserID == request.UserId 
                                      && !r.IsDeleted, ct);

        if (existingReaction is not null)
        {
            // Update existing reaction type
            existingReaction.Type = type;
            existingReaction.DateTimeAddedUTC = clock.GetUtcNow().UtcDateTime;
            await ctx.SaveChangesAsync(ct);
            return existingReaction.ReactionID;
        }

        var entity = new ReactionEntity
        {
            ReactionID = 0,
            BlogPostID = request.BlogPostId,
            UserID = request.UserId,
            Type = type,
            DateTimeAddedUTC = clock.GetUtcNow().UtcDateTime,
            BlogPost = null!,
            User = null!,
            IsDeleted = false
        };

        ctx.Reactions.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.ReactionID;
    }
}
