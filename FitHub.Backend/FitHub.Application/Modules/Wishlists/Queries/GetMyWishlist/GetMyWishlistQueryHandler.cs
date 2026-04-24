using FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Wishlists.Queries.GetMyWishlist
{
    public sealed class GetMyWishlistQueryHandler(IAppDbContext ctx, IAppCurrentUser user)
        : IRequestHandler<GetMyWishlistQuery, PageResult<GetMyWishlistQueryDto>>
    {
        public async Task<PageResult<GetMyWishlistQueryDto>> Handle(GetMyWishlistQuery request, CancellationToken cancellationToken)
        {
            var userId = user.UserId ?? throw new UnauthorizedAccessException("Please sign in.");

            var query = ctx.WishListItems
                .AsNoTracking()
                .Include(x => x.FitnessPlan)
                .Where(x => x.WishList.UserID == userId && !x.WishList.IsDeleted)
                .OrderByDescending(x => x.AddedAt)
                .Select(x => new GetMyWishlistQueryDto
                {
                    WishListItemID = x.WishListItemID,
                    FitnessPlanID = x.FitnessPlanID,
                    PlanTitle = x.FitnessPlan.Title,
                    Price = x.FitnessPlan.Price,
                    Difficulty = x.FitnessPlan.Difficulty,
                    AddedAt = x.AddedAt
                });


            return await PageResult<GetMyWishlistQueryDto>.FromQueryableAsync(query, request.Paging, cancellationToken);
        }
    }
}
