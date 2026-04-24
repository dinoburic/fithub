using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Audit
{
    public class AuditLog
    {
        [Key]
        public required int LogID { get; set; }
        public required string? Content { get; set; }
        public required DateTime DatumVrijemeUTC { get; set; }
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required string Type { get; set; }
        public required int CenterID { get; set; }
        public required FitnessCenters FitnessCenter { get; set; }
    }
}
