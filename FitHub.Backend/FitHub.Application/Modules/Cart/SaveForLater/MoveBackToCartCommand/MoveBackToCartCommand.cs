using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.SaveForLater.MoveBackToCartCommand
{
    public class MoveBackToCartCommand : IRequest<Unit>
    {
        public int CartItemID { get; set; }
    }
}
