using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Training
{
    public class Enrollments
    {
        [Key]
        public required int EnrollmentID { get; set; }
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required int FitnessPlanID { get; set; }
        public required FitnessPlans FitnessPlan { get; set; }
        public required DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public required string Status { get; set; }
        public required int CenterID { get; set; }
        public required FitnessCenters Center { get; set; }
        public required bool IsDeleted { get; set; }
        public int? CompletedDays { get; set; }
        public int? CurrentDay { get; set; }
        public int? TotalDays { get; set; }
        public bool? IsPaused { get; set; }
        public DateTime? LastActivityAt { get; set; }


    }
}
