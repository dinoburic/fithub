using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Queries.GetMyCart
{
    public class GetMyCartQueryDto
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int CenterID { get; set; }
        public DateTime CreatedAt { get; set; }
        public required bool IsDeleted { get; set; }
        public decimal SubTotal { get; set; }
        public List<GetMyCartItemQueryDto> CartItems { get; set; } = [];

    }

    public class GetMyCartItemQueryDto
    {
        public int CartItemID { get; set; }
        public int FitnessPlanID { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsSavedForLater { get; set; }
    }
}
   
