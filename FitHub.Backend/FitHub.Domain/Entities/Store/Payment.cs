using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Store
{
    public class Payment
    {
        [Key]
       public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public Orders Order { get; set; } 
        
        public string StripeSessionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } 
        
        public DateTime CreatedAtUtc { get; set; }

    }
}
