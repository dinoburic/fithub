using FitHub.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FitHub.Application.Common.Exceptions;

namespace FitHub.Application.Modules.Training.FitnessPlans.Commands.Delete;

public sealed class DeleteFitnessPlanCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteFitnessPlanCommand>
{
    public async Task<Unit> Handle(DeleteFitnessPlanCommand request, CancellationToken ct)
    {
        var entity = await ctx.FitnessPlans.FirstOrDefaultAsync(x => x.PlanID == request.Id, ct);
        if (entity is null) throw new MarketNotFoundException($"Fitness plan (ID={request.Id}) doesn't exist.");

        entity.IsDeleted = true;
        await ctx.SaveChangesAsync(ct);
        return Unit.Value;
    }

    Task IRequestHandler<DeleteFitnessPlanCommand>.Handle(DeleteFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
