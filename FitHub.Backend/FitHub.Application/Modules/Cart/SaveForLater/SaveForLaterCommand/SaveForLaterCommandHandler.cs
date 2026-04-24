using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.SaveForLater.SaveForLaterCommand
{
    public class SaveForLaterCommandHandler( IAppDbContext ctx) : IRequestHandler<SaveForLaterCommand, Unit>
    {
        public Task<Unit> Handle(SaveForLaterCommand request, CancellationToken cancellationToken)
        {
           var cartItem = ctx.CartItems.Where(ci => ci.CartItemID == request.CartItemID && !ci.IsDeleted).FirstOrDefault();

            if(cartItem == null)
            {
                throw new MarketNotFoundException("Cart item not found");
            }

            if(cartItem.IsSavedForLater)
            {
                throw new MarketConflictException("Cart item is already saved for later");
            }

            cartItem.IsSavedForLater = true;
            ctx.CartItems.Update(cartItem);
            ctx.SaveChangesAsync(cancellationToken);
            return Task.FromResult(Unit.Value);
        }
    }
}
