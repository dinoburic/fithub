using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.GetByPlanId
{
    public class GetReviewsByPlanIdQuery : IRequest<List<GetReviewsByPlanIdQueryDto>>
    {
        public int FitnessPlanID { get; set; }
    }
}
