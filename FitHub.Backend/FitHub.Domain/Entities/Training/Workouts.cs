using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Training
{
    public class Workouts
    {
        [Key]
        public required int WorkoutID { get; set; }
        public required string Name { get; set; }
        public required int DurationinMinutes { get; set; }
        public required string? Description { get; set; }
        public required float? Difficulty { get; set; }
        public DateTime? UpdatedAtUTC { get; set; }
        public required int CenterID { get; set; }
        public required FitnessCenters Center { get; set; }
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required int? NumberOfRepetitions { get; set; }
        public required int? NumberOfSeries { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
