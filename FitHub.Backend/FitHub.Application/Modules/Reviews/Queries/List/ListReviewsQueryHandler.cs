using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.List
{
    public sealed class ListReviewsQueryHandler(IAppDbContext ctx)
        : IRequestHandler<ListReviewsQuery, PageResult<ListReviewsQueryDto>>
    {
        public async Task<PageResult<ListReviewsQueryDto>> Handle(
            ListReviewsQuery request, CancellationToken ct)
        {
            var q = ctx.Reviews.AsNoTracking().Where(x => !x.IsDeleted);

         

            if (request.FitnessPlanID is not null)
                q = q.Where(x => x.FitnessPlanID == request.FitnessPlanID);

            if (request.CenterID is not null)
                q = q.Where(x => x.CenterID == request.CenterID);

            var projectedQuery = q.OrderByDescending(x => x.CreatedAtUTC)
                .Select(x => new ListReviewsQueryDto
                {
                    ReviewID = x.ReviewID,
                    Rating = x.Rating,
                    Comment = x.Comment,
                    FitnessPlanID = x.FitnessPlanID,
                    UserName = x.User.Name,
                    CreatedAtUTC = x.CreatedAtUTC
                });

            return await PageResult<ListReviewsQueryDto>.FromQueryableAsync(projectedQuery,request.Paging, ct);
        }
    }
}
