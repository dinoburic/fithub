using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace FitHub.Domain.Entities.Training
{
    public class FitnessPlans
    {
        [Key]
        public int PlanID { get; set; }
        public required string Title { get; set; }
        public required string? Description { get; set; }
        public required string? Difficulty { get; set; }
        public required float Price { get; set; }
        public required int? DailyDurationInMinutes { get; set; }
        public required int? DurationInWeeks { get; set; }
        public required int? ReviewsNumber { get; set; }
        public required float? AverageRating { get; set; }
        public required int CenterID { get; set; }
        public FitnessCenters? Center { get; set; }
        public required int FitnessPlanTypeID { get; set; }
        public FitnessPlanTypes? FitnessPlanType { get; set; }
        public required int UserID { get; set; }
        public Users? User { get; set; }
        public required bool IsDeleted { get; set; }
        public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAtUtc { get; set; }
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

    }
}
