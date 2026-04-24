using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Training
{
    public class Ratings
    {
        [Key]
        public required int RatingID { get; set; }
        public required int Rating { get; set; }
        public required int WorkoutID { get; set; }
        public required Workouts Workout { get; set; }
        public required int CenterID { get; set; }
        public required FitnessCenters Center { get; set; }
        public required string? Comment { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
