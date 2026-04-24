using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Training
{
    public class UsersFitnessPlanTypes
    {
        [Key]
        public required int UsersFitnessPlanTypesID { get; set; }
        public required int UserID { get; set; }
        public required Users User { get; set; }
        
        public required int FitnessPlanTypeID { get; set; }
        public required FitnessPlanTypes FitnessPlanType { get; set; }
    }
}
