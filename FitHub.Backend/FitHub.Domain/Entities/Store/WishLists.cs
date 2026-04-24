using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Store
{
    public class WishLists
    {
        [Key]
        public int WishListID { get; set; }
        public int UserID { get; set; }
        public Users User { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public int CenterID { get; set; }
        public FitnessCenters Center { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
