using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Commands.Create
{
    public class CreateFitnessPlanCommand : IRequest<int>
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Difficulty { get; set; }

        [Required]
        public float Price { get; set; }

        public int? DailyDurationInMinutes { get; set; }

        public int? DurationInWeeks { get; set; }

        [Required]
        public int CenterID { get; set; }

        [Required]
        public int FitnessPlanTypeID { get; set; }

        [Required]
        public int UserID { get; set; }
        [Required]
        public DateTime CreatedAtUtc { get; set; }
    }
}
