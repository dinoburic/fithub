using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Communication
{
    public class Notifications
    {
        [Key]
        public required int NotificationID { get; set; }
        public required string Title { get; set; }
        public required string? Description { get; set; }
        public required bool? IsRead { get; set; }
        public required int CenterID { get; set; }
        public required FitnessCenters FitnessCenter { get; set; }
        public required int UserSenderID { get; set; }
        public required Users UserSender { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
