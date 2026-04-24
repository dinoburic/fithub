using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest<int>
    {
        public int FitnessPlanID { get; set; }
        public int Quantity { get; set; }
    }
}
