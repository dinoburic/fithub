using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Training
{
    public class Reviews
    {
        [Key]
        public int ReviewID { get; set; }
        public required int Rating { get; set; }
        public string? Comment { get; set; }
        public required int FitnessPlanID { get; set; }
        public  FitnessPlans FitnessPlan { get; set; }
        public required int CenterID { get; set; }
        public  FitnessCenters Center { get; set; }
        public required int UserID { get; set; }
        public  Users User { get; set; }
        public required DateTime CreatedAtUTC { get; set; }
        public DateTime? UpdatedAtUTC { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
