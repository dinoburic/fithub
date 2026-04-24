using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Domain.Entities.Identity
{
    public class FitnessCenters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CenterID { get; set; }
        public required string Name { get; set; }
        public required string? Location { get; set; }
        public required int? Capacity { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
