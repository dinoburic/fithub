using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Store
{
    public class Carts
    {
        [Key]
        public int CartID { get; set; }
        public int UserID { get; set; }
        public Users User { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CenterID { get; set; }
        public FitnessCenters Center { get; set; }
        public required bool IsDeleted { get; set; }
        public decimal SubTotal { get; set; }
        public ICollection<CartItems> Items { get; set; } = new List<CartItems> { };

    }
}
