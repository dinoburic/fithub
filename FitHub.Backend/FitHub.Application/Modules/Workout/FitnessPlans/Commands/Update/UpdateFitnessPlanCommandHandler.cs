using FitHub.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FitHub.Application.Common.Exceptions;
using FitHub.Domain.Entities.Training;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Commands.Update;

public sealed class UpdateFitnessPlanCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateFitnessPlanCommand>
{
    public async Task<Unit> Handle(UpdateFitnessPlanCommand request, CancellationToken ct)
    {
        var entity = await ctx.FitnessPlans.FirstOrDefaultAsync(x => x.PlanID == request.Id, ct);
        if (entity is null) throw new MarketNotFoundException($"Fitness plan (ID={request.Id}) doesn't exist");

        entity.Title = request.Title.Trim();
        entity.Description = request.Description;
        entity.Difficulty = request.Difficulty;
        entity.Price = request.Price;
        entity.DailyDurationInMinutes = request.DailyDurationInMinutes;
        entity.DurationInWeeks = request.DurationInWeeks;
        entity.ReviewsNumber = request.ReviewsNumber;
        entity.AverageRating = request.AverageRating ?? entity.AverageRating;

        await ctx.SaveChangesAsync(ct);
        return Unit.Value;
    }

    Task IRequestHandler<UpdateFitnessPlanCommand>.Handle(UpdateFitnessPlanCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
