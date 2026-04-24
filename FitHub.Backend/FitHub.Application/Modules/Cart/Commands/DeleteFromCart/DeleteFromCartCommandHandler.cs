using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.DeleteFromCart
{
    public class DeleteFromCartCommandHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<DeleteFromCartCommand, int>
    {
        public async Task<int> Handle(DeleteFromCartCommand request, CancellationToken cancellationToken)
        {
            var userID=user.UserId;

            var cart = ctx.Carts
                .Where(x => x.UserID == userID && !x.IsDeleted)
                .FirstOrDefault();

            if (cart is null)
            {
                throw new MarketNotFoundException("Cart not found for the current user.");
            }

            var cartItem = ctx.CartItems
                .Where(x => x.CartItemID == request.CartItemID && !x.IsDeleted && x.CartID == cart.CartID)
                .FirstOrDefault();

            if (cartItem is null)
            {
                throw new MarketNotFoundException("Cart item not found in the user's cart.");
            }

            cartItem.IsDeleted = true;

            await ctx.SaveChangesAsync(cancellationToken);

            return cart.CartID;
        }
    }
}
