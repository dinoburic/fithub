using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Wishlists.Commands.Remove
{
    public sealed class RemoveWishListItemCommandHandler(IAppDbContext ctx, IAppCurrentUser user)
        : IRequestHandler<RemoveWishListItemCommand>
    {
        public async Task Handle(RemoveWishListItemCommand request, CancellationToken cancellationToken)
        {
            var userId = user.UserId ?? throw new UnauthorizedAccessException("Please sign in.");

            var itemToRemove = await ctx.WishListItems
                .FirstOrDefaultAsync(i => i.FitnessPlanID == request.FitnessPlanID
                                       && i.WishList.UserID == userId
                                       && !i.WishList.IsDeleted,
                                     cancellationToken);

            if (itemToRemove == null)
            {
                throw new KeyNotFoundException("This plan is not in your wishlist.");
            }

            ctx.WishListItems.Remove(itemToRemove);
            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
