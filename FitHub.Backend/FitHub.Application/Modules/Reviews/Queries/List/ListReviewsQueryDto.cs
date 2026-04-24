using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.List
{
    public class ListReviewsQueryDto
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int FitnessPlanID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAtUTC { get; set; }
    }
}
