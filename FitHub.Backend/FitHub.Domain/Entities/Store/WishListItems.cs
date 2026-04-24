using FitHub.Domain.Entities.Training;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Store
{
    public class WishListItems
    {
        [Key]
        public int WishListItemID { get; set; }
        public int WishListID { get; set; }
        public WishLists WishList { get; set; }
        public int FitnessPlanID { get; set; }
        public FitnessPlans FitnessPlan { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<WishListItems> Items { get; set; } = new List<WishListItems>();
    }
}
