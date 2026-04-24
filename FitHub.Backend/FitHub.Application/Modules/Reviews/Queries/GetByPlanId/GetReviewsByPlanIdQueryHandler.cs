using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.GetByPlanId
{
    public sealed class GetReviewsByPlanIdQueryHandler(IAppDbContext ctx)
         : IRequestHandler<GetReviewsByPlanIdQuery, List<GetReviewsByPlanIdQueryDto>>
    {
        public async Task<List<GetReviewsByPlanIdQueryDto>> Handle(GetReviewsByPlanIdQuery request, CancellationToken ct)
        {
            return await ctx.Reviews
                .AsNoTracking()
                .Where(x => x.FitnessPlanID == request.FitnessPlanID && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAtUTC)
                .Select(x => new GetReviewsByPlanIdQueryDto
                {
                    ReviewID = x.ReviewID,
                    Rating = x.Rating,
                    Comment = x.Comment,
                    FitnessPlanID = x.FitnessPlanID,
                    UserID = x.UserID,
                    UserName = x.User.Name,
                    CreatedAtUTC = x.CreatedAtUTC
                })
                .ToListAsync(ct);
        }
    }
}
