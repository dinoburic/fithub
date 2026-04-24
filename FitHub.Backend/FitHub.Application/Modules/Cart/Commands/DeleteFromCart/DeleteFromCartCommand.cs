using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.DeleteFromCart
{
    public class DeleteFromCartCommand : IRequest<int>
    {
        public int CartItemID { get; set; }
    }
}
