using FluentValidation;
using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Update;

public sealed class UpdateBlogPostCommandHandler(IAppDbContext ctx, TimeProvider clock) : IRequestHandler<UpdateBlogPostCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBlogPostCommand request, CancellationToken ct)
    {
        var entity = await ctx.BlogPosts.FirstOrDefaultAsync(b => b.BlogPostID == request.Id, ct);
        if (entity is null)
            throw new MarketNotFoundException($"Blog post (ID={request.Id}) doesn't exist.");

        var title = request.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
            throw new ValidationException("Title is required.");

        bool categoryExists = await ctx.Categories
            .AsNoTracking()
            .AnyAsync(c => c.CategoryID == request.CategoryId, ct);
        if (!categoryExists)
            throw new MarketNotFoundException($"Category (ID={request.CategoryId}) doesn't exist.");

        entity.Title = title;
        entity.Content = string.IsNullOrWhiteSpace(request.Content) ? null : request.Content.Trim();
        entity.CategoryID = request.CategoryId;
        entity.UpdatedAtUTC = clock.GetUtcNow().UtcDateTime;

        await ctx.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
