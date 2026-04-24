using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.Reactions.Commands.Delete;

public sealed class DeleteReactionCommandHandler(IAppDbContext ctx) 
    : IRequestHandler<DeleteReactionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteReactionCommand request, CancellationToken ct)
    {
        var entity = await ctx.Reactions.FirstOrDefaultAsync(r => r.ReactionID == request.Id && !r.IsDeleted, ct);
        if (entity is null)
            throw new MarketNotFoundException($"Reaction (ID={request.Id}) doesn't exist.");

        entity.IsDeleted = true;
        await ctx.SaveChangesAsync(ct);

        return Unit.Value;
    }
}
