using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Wishlists.Commands.Remove
{
    public class RemoveWishListItemCommand : IRequest
    {
        public int FitnessPlanID { get; set; }
    }
}
