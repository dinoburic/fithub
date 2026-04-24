using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Training
{
    public class Exercises
    {
        [Key]
        public int ExerciseID { get; set; }
        public required string Title { get; set; }
        public required string? Description { get; set; }
        public required string? ImageURL { get; set; }
        public required string? VideoURL { get; set; }
        public int CenterID { get; set; }
        public FitnessCenters Center { get; set; }
        public int UserID { get; set; } //trener
        public Users User { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
