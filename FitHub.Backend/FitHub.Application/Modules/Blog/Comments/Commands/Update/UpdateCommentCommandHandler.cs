using FluentValidation;
using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Blog.Comments.Commands.Update;

public sealed class UpdateCommentCommandHandler(IAppDbContext ctx, TimeProvider clock) 
    : IRequestHandler<UpdateCommentCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken ct)
    {
        var entity = await ctx.Comments.FirstOrDefaultAsync(c => c.CommentID == request.Id && !c.IsDeleted, ct);
        if (entity is null)
            throw new MarketNotFoundException($"Comment (ID={request.Id}) doesn't exist.");

        var content = request.Content?.Trim();
        if (string.IsNullOrWhiteSpace(content))
            throw new ValidationException("Content is required.");

        entity.Content = content;

        await ctx.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
