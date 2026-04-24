using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Communication
{
    public class ChatsUsers
    {
        [Key]
        public required int ChatsUsersID { get; set; }
        public required int ChatID { get; set; }
        public required Chats Chat { get; set; }
        
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
