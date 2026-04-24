using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Identity
{
    public class UserBadges
    {
        [Key]
        public required int BadgeID { get; set; }
        public required string Name { get; set; }
    }
}
