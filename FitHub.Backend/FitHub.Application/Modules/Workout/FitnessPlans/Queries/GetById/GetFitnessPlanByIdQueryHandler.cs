using FitHub.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FitHub.Application.Common.Exceptions;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.GetById;

public sealed class GetFitnessPlanByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetFitnessPlanByIdQuery, GetFitnessPlanByIdQueryDto>
{
    public async Task<GetFitnessPlanByIdQueryDto> Handle(GetFitnessPlanByIdQuery request, CancellationToken ct)
    {
        var dto = await ctx.FitnessPlans
            .Where(x => x.PlanID == request.Id)
            .Select(x => new GetFitnessPlanByIdQueryDto
            {
                PlanID = x.PlanID,
                Title = x.Title,
                Description = x.Description,
                Difficulty = x.Difficulty,
                Price = x.Price,
                DailyDurationInMinutes = x.DailyDurationInMinutes,
                DurationInWeeks = x.DurationInWeeks,
                ReviewsNumber = x.ReviewsNumber,
                AverageRating = x.AverageRating,
                CenterID = x.CenterID,
                FitnessPlanTypeID = x.FitnessPlanTypeID,
                UserID = x.UserID,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .FirstOrDefaultAsync(ct);

        if (dto is null) throw new MarketNotFoundException($"Fitness plan (ID={request.Id}) doesn't exist.");

        return dto;
    }
}
