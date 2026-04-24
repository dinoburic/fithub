using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.Recommendations
{
    public class GetRecommendedPlansQuery : IRequest<List<ListFitnessPlansQueryDto>>
    {
        public int Count { get; set; } = 4; 
    }
}
