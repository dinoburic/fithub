using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.SaveForLater.MoveBackToCartCommand
{
    public class MoveBackToCartCommandHandler(IAppDbContext ctx) : IRequestHandler<MoveBackToCartCommand, Unit>
    {
        public Task<Unit> Handle(MoveBackToCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = ctx.CartItems.Where(ctx => ctx.CartItemID == request.CartItemID && !ctx.IsDeleted).FirstOrDefault();
       
            if (cartItem == null)
            {
                throw new MarketNotFoundException("Cart item not found");
            }

            if (!cartItem.IsSavedForLater)
            {
                throw new MarketConflictException("Cart item is already in the cart");
            }
            cartItem.IsSavedForLater = false;
            ctx.CartItems.Update(cartItem);
            ctx.SaveChangesAsync(cancellationToken);
            return Task.FromResult(Unit.Value);
        }
    }
}
