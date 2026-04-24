using FitHub.Application.Abstractions;
using MediatR;
using FitHub.Domain.Entities.Training;
using System.ComponentModel.DataAnnotations;

using ValidationException = FluentValidation.ValidationException;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Commands.Create;

public class CreateFitnessPlanCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateFitnessPlanCommand, int>
{
    public async Task<int> Handle(CreateFitnessPlanCommand request, CancellationToken ct)
    {
        var title = request.Title?.Trim();
        if (string.IsNullOrWhiteSpace(title))
            throw new ValidationException("Title is required.");

        if (request.Price < 0)
            throw new ValidationException("Price must be non-negative.");

        var entity = new FitHub.Domain.Entities.Training.FitnessPlans
        {
            Title = title,
            Description = request.Description,
            Difficulty = request.Difficulty,
            Price = request.Price,
            DailyDurationInMinutes = request.DailyDurationInMinutes,
            DurationInWeeks = request.DurationInWeeks,
            ReviewsNumber = 0,
            AverageRating = 0,
            CenterID = request.CenterID,
            FitnessPlanTypeID = request.FitnessPlanTypeID,
            UserID = request.UserID,
            IsDeleted = false,
            CreatedAtUtc = request.CreatedAtUtc
        };

        ctx.FitnessPlans.Add(entity);
        await ctx.SaveChangesAsync(ct);

        return entity.PlanID;
    }
}
