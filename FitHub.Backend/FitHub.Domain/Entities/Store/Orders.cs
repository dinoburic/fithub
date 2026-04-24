using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Domain.Entities.Store
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int CenterID { get; set; }
        [ForeignKey("CenterID")]
        public FitnessCenters Center { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Users User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? PaidAtUtc { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";

        public bool IsDeleted { get; set; } = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }

        public ICollection<OrderItems> Items { get; set; } = new List<OrderItems>();
    }
}
