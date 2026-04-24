using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Communication
{
    public class Chats
    {
        [Key]
        public required int ChatID { get; set; }
        public required DateTime StartedAt { get; set; }
        public required bool IsActive { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
