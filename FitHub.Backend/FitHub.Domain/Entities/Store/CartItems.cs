using FitHub.Domain.Entities.Training;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Store
{
    public class CartItems
    {
        [Key]
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public Carts Cart { get; set; }
        public int FitnessPlanID { get; set; }
        public FitnessPlans FitnessPlan { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required bool IsDeleted { get; set; }
        public bool IsSavedForLater { get; set; } = false;
    }
}
