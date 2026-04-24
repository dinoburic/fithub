using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.SaveForLater.SaveForLaterCommand
{
    public class SaveForLaterCommand : IRequest<Unit>
    {
        public int CartItemID { get; set; }
    }
}
