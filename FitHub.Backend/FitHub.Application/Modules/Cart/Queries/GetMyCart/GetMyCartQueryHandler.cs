using FitHub.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Queries.GetMyCart
{
    public class GetMyCartQueryHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<GetMyCartQuery,GetMyCartQueryDto>
    {
        public async Task<GetMyCartQueryDto> Handle(GetMyCartQuery request, CancellationToken cancellationToken)
        {
           
            var entity = await ctx.Carts
                .Where(x => x.UserID == user.UserId && !x.IsDeleted)
                .Select(x => new GetMyCartQueryDto
                {
                    CartID = x.CartID,
                    UserID=user.UserId ?? 0,
                    IsDeleted = x.IsDeleted,
                    CartItems = x.Items.Where(i => i.IsDeleted == false).Select(i => new GetMyCartItemQueryDto
                    {
                        CartItemID = i.CartItemID,
                        FitnessPlanID = i.FitnessPlanID,
                        Price=i.Price,
                        Quantity=i.Quantity,
                        IsSavedForLater = i.IsSavedForLater

                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                throw new MarketNotFoundException("Cart not found for the current user.");
            }
            entity.SubTotal=entity.CartItems.Sum(i => i.Price * i.Quantity);
            return entity;
        }
    }
}
