using FitHub.Application.Modules.MachineLearning.Models;
using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.Recommendations
{
    public sealed class GetRecommendedPlansQueryHandler(
        IAppDbContext ctx,
        IAppCurrentUser user,
        PredictionEnginePool<FitnessPlanRating, FitnessPlanPrediction> predictionEngine
        ) 
        : IRequestHandler<GetRecommendedPlansQuery, List<ListFitnessPlansQueryDto>>
    {
        public async Task<List<ListFitnessPlansQueryDto>> Handle(GetRecommendedPlansQuery request, CancellationToken ct)
        {
            var userId = user.UserId ?? throw new UnauthorizedAccessException();

            var unreadPlans = await ctx.FitnessPlans
                .Where(p => !p.IsDeleted && p.CenterID==user.CenterId) 
                .Select(p => p.PlanID)
                .ToListAsync(ct);

            var scoredPlans = new List<Tuple<int, float>>();

            // 2. ML.NET Predikcija
            // Pitamo model: "Koju bi ocjenu korisnik X dao programu Y?"
            foreach (var planId in unreadPlans)
            {
                var input = new FitnessPlanRating { UserId = userId, PlanId = planId };

                var prediction = predictionEngine.Predict(input);

                scoredPlans.Add(new Tuple<int, float>(planId, prediction.Score));
            }

            // 3. Take top 4 with the highest 'Score'
            var topPlanIds = scoredPlans
                .OrderByDescending(x => x.Item2)
                .Take(request.Count)
                .Select(x => x.Item1)
                .ToList();

            // 4. Return them from the database
            var recommendations = await ctx.FitnessPlans
             .Where(p => topPlanIds.Contains(p.PlanID))
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
                 AverageRating = (float?)x.Reviews.Where(r => !r.IsDeleted).Average(r => (float?)r.Rating),
                 CenterID = x.CenterID,
                 FitnessPlanTypeID = x.FitnessPlanTypeID,
                 UserID = x.UserID,
                 CreatedAtUtc = x.CreatedAtUtc
             })
             .ToListAsync(ct);

         return recommendations;
        }
    }
}
