using FitHub.Domain.Entities.Training;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Domain.Entities.Store
{
    public class OrderItems
    {
        [Key]
        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Orders Order { get; set; }

        public int FitnessPlanID { get; set; }
        public FitnessPlans FitnessPlan { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
