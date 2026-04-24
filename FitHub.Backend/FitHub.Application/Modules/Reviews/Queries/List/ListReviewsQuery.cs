using FitHub.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.List
{
    public class ListReviewsQuery : BasePagedQuery<ListReviewsQueryDto>
    {
        public int? FitnessPlanID { get; set; }
        public int? CenterID { get; set; }
    }
}
