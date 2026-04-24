using FitHub.Domain.Entities.Identity;
using FitHub.Domain.Entities.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Orders.Commands.Create
{
    public sealed class CreateOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<CreateOrderCommand, int>
    {
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
           if (user.UserId is null)
                throw new UnauthorizedAccessException("Niste prijavljeni.");

            var cart = await ctx.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.FitnessPlan) // We have access to the latest plan and its price
                .FirstOrDefaultAsync(x => x.UserID == user.UserId && !x.IsDeleted, cancellationToken);

            if (cart == null) throw new KeyNotFoundException("Cart does not exist.");

            var activeItems = cart.Items
                .Where(i => !i.IsSavedForLater && !i.IsDeleted)
                .ToList();

            if (!activeItems.Any()) throw new InvalidOperationException("Cart is empty.");

            var firstItemCenterId = activeItems.First().FitnessPlan.CenterID;

            bool hasMixedCenters = activeItems.Any(x => x.FitnessPlan.CenterID != firstItemCenterId);

            if (hasMixedCenters)
            {
                throw new InvalidOperationException("Please buy items from only one fitness center");
            }

            var newOrder = new Domain.Entities.Store.Orders
            {
                UserID = user.UserId.Value,
                CenterID = firstItemCenterId,
                CreatedAt = DateTime.UtcNow,
                Status = "Draft",
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = request.Address,
                City = request.City,
                ZipCode = request.ZipCode,
                Country = request.Country,

                // FIXED: Use current price from FitnessPlan table instead of cached CartItems value
                TotalAmount = activeItems.Sum(x => (decimal)x.FitnessPlan.Price * x.Quantity)
            };

            foreach (var cartItem in activeItems)
            {
                newOrder.Items.Add(new OrderItems
                {
                    FitnessPlanID = cartItem.FitnessPlanID,
                    Quantity = cartItem.Quantity,
                    
                    // FIXED: Store current plan price at purchase time
                    Price = (decimal)cartItem.FitnessPlan.Price 
                });
            }

            ctx.Orders.Add(newOrder);
            ctx.CartItems.RemoveRange(activeItems); 

            await ctx.SaveChangesAsync(cancellationToken);

            return newOrder.OrderID;
        }
    }
}
