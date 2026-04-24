using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Wishlists.Queries.GetMyWishlist
{
    public class GetMyWishlistQueryDto
    {
        public int WishListItemID { get; set; }
        public int FitnessPlanID { get; set; }
        public string PlanTitle { get; set; } = string.Empty;
        public float Price { get; set; }
        public string? Difficulty { get; set; } 
        public DateTime AddedAt { get; set; }
    }
}
