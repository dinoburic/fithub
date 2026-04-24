using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.AddToCart
{
    public class AddToCartCommandDto
    {
        public int CartID { get; set; }
        public int CartItemID { get; set; }
        public int FitnessPlanID { get; set; }
    }
}
