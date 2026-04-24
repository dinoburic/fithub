using FitHub.Application.Modules.Users.Queries.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.FitnessCenters.Queries.List
{
    public class ListFitnessCentersQueryHandler(IAppDbContext ctx)
        : IRequestHandler<ListFitnessCentersQuery, PageResult<ListFitnessCentersQueryDto>>
    {
        public async Task<PageResult<ListFitnessCentersQueryDto>> Handle(
       ListFitnessCentersQuery request, CancellationToken ct)
        {
            var q = ctx.FitnessCenters.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                q = q.Where(x => x.Name.Contains(request.Search));
            }

            var projectedQuery = q.OrderBy(x => x.Name)
                .Select(x => new ListFitnessCentersQueryDto
                {
                    Id = x.CenterID,
                    Name = x.Name

                });

            return await PageResult<ListFitnessCentersQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
        }
    }
}
