using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Store
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public Orders Order { get; set; }
        public float Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string? Currency { get; set; }
    }
}
