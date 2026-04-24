using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Wishlists.Commands.AddWishlistItem
{
    public class AddWishListItemCommand : IRequest<int>
    {
        public int FitnessPlanID { get; set; }
    }
}
