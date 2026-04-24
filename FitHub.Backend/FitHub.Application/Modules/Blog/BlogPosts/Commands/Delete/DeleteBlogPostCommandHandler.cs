using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Delete;

public sealed class DeleteBlogPostCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteBlogPostCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBlogPostCommand request, CancellationToken ct)
    {
        var entity = await ctx.BlogPosts.FirstOrDefaultAsync(b => b.BlogPostID == request.Id, ct);
        if (entity is null)
            throw new MarketNotFoundException($"Blog post (ID={request.Id}) doesn't exist.");

        entity.IsDeleted = true;
        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
