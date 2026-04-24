using FitHub.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Wishlists.Commands.AddWishlistItem
{
    public sealed class AddWishListItemCommandHandler(IAppDbContext ctx, IAppCurrentUser user)
         : IRequestHandler<AddWishListItemCommand, int>
    {
        public async Task<int> Handle(AddWishListItemCommand request, CancellationToken cancellationToken)
        {
            var userId = user.UserId ?? throw new UnauthorizedAccessException("Niste prijavljeni.");

            var plan = await ctx.FitnessPlans
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PlanID == request.FitnessPlanID, cancellationToken)
                ?? throw new KeyNotFoundException("Fitness plan does not exist.");

            var wishList = await ctx.WishLists
                .FirstOrDefaultAsync(w => w.UserID == userId && !w.IsDeleted, cancellationToken);

            if (wishList == null)
            {
                wishList = new WishLists
                {
                    UserID = userId,
                    CenterID = user.CenterId ?? 1,
                    CreatedAt = DateTime.UtcNow,
                    Title = "My Wishlist",
                    Status = "Active",
                    IsDeleted = false
                };
                ctx.WishLists.Add(wishList);
                await ctx.SaveChangesAsync(cancellationToken); 
            }

            bool alreadyExists = await ctx.WishListItems
                .AnyAsync(i => i.WishListID == wishList.WishListID && i.FitnessPlanID == request.FitnessPlanID, cancellationToken);

            if (alreadyExists)
            {
                throw new InvalidOperationException("This plan is already in your wishlist.");
            }

            var newItem = new WishListItems
            {
                WishListID = wishList.WishListID,
                FitnessPlanID = request.FitnessPlanID,
                AddedAt = DateTime.UtcNow
            };

            ctx.WishListItems.Add(newItem);
            await ctx.SaveChangesAsync(cancellationToken);

            return newItem.WishListItemID;
        }
    }
}
