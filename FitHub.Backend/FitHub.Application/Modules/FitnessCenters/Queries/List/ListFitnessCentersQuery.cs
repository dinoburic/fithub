using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.FitnessCenters.Queries.List
{
    public class ListFitnessCentersQuery : BasePagedQuery<ListFitnessCentersQueryDto>
    {
        public string? Search { get; init; }
    }
}
