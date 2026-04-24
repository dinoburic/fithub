using FluentValidation;
using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using BlogPostEntity = FitHub.Domain.Entities.Blog.BlogPosts;

namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Create;

public sealed class CreateBlogPostCommandHandler(IAppDbContext ctx, TimeProvider clock)
    : IRequestHandler<CreateBlogPostCommand, int>
{
    public async Task<int> Handle(CreateBlogPostCommand request, CancellationToken ct)
    {
        var title = request.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
            throw new ValidationException("Title is required.");

        bool categoryExists = await ctx.Categories
            .AsNoTracking()
            .AnyAsync(c => c.CategoryID == request.CategoryId, ct);
        if (!categoryExists)
            throw new MarketNotFoundException($"Category (ID={request.CategoryId}) doesn't exist.");

        bool userExists = await ctx.Users
            .AsNoTracking()
            .AnyAsync(u => u.UserID == request.UserId, ct);
        if (!userExists)
            throw new MarketNotFoundException($"User (ID={request.UserId}) doesn't exist.");

        var entity = new BlogPostEntity
        {
            BlogPostID = 0, 
            Title = title,
            Content = string.IsNullOrWhiteSpace(request.Content) ? null : request.Content.Trim(),
            CategoryID = request.CategoryId,
            UserID = request.UserId,
            CreatedAtUTC = clock.GetUtcNow().UtcDateTime,
            UpdatedAtUTC = null,
            Category = null!,
            User = null!,
            IsDeleted = false
        };

        ctx.BlogPosts.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.BlogPostID;
    }
}
