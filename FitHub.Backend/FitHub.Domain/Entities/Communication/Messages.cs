using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Communication
{
    public class Messages
    {
        [Key]
        public required int MessageID { get; set; }
        public required int ChatID { get; set; }
        public required Chats Chat { get; set; }
        public required int UserSenderID { get; set; }
        public required Users User { get; set; }
        public required string Content { get; set; }
        public required DateTime SentAt { get; set; }
        public required string Status { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
