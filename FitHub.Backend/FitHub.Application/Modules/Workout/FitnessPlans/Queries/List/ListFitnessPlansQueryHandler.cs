using FitHub.Application.Abstractions;
using FitHub.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;

public sealed class ListFitnessPlansQueryHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<ListFitnessPlansQuery, PageResult<ListFitnessPlansQueryDto>>
{
    public async Task<PageResult<ListFitnessPlansQueryDto>> Handle(ListFitnessPlansQuery request, CancellationToken ct)
    {
        var query = ctx.FitnessPlans.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var s = request.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.Title.ToLower().Contains(s) || (x.Description != null && x.Description.ToLower().Contains(s)));
        }

        if (user != null)
        {
            var centerId = user.CenterId;
            if (centerId.HasValue)
            {
                query = query.Where(x => x.CenterID == centerId.Value);
            }
        }

        if (request.CenterID.HasValue)
        {
            query = query.Where(x => x.CenterID == request.CenterID.Value);
        }

        if (request.FitnessPlanTypeID.HasValue)
        {
            query = query.Where(x => x.FitnessPlanTypeID == request.FitnessPlanTypeID.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Difficulty))
        {
            var difficulty = request.Difficulty.Trim().ToLowerInvariant();
            query = query.Where(x => x.Difficulty.ToLower() == difficulty);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= request.MaxPrice.Value);
        }

        if (request.CreatedFrom.HasValue)
        {
            query = query.Where(x => x.CreatedAtUtc >= request.CreatedFrom.Value);
        }

        if (request.CreatedTo.HasValue)
        {
            query = query.Where(x => x.CreatedAtUtc <= request.CreatedTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            query = request.OrderBy switch
            {
                "price_asc" => query.OrderBy(fp => fp.Price),
                "price_desc" => query.OrderByDescending(fp => fp.Price),
                "date_asc" => query.OrderBy(fp => fp.CreatedAtUtc),
                "date_desc" => query.OrderByDescending(fp => fp.CreatedAtUtc),
                _ => query.OrderBy(fp => fp.Title)   
            };
        }
        else
        {
            query = query.OrderBy(fp => fp.PlanID);
        }

        var dtoQuery = query
            .Select(x => new ListFitnessPlansQueryDto
            {
                PlanID = x.PlanID,
                Title = x.Title,
                Description = x.Description,
                Difficulty = x.Difficulty,
                Price = x.Price,
                DailyDurationInMinutes = x.DailyDurationInMinutes,
                DurationInWeeks = x.DurationInWeeks,
                ReviewsNumber = x.Reviews.Count(r => !r.IsDeleted),
                AverageRating = x.Reviews.Where(r => !r.IsDeleted).Average(r => (float?)r.Rating),
                CenterID = x.CenterID,
                FitnessPlanTypeID = x.FitnessPlanTypeID,
                UserID = x.UserID,
                CreatedAtUtc = x.CreatedAtUtc
            });

        return await PageResult<ListFitnessPlansQueryDto>.FromQueryableAsync(dtoQuery, request.Paging, ct);
    }
}