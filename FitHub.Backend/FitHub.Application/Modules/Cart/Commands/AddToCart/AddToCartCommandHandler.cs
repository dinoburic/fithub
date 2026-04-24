using Microsoft.EntityFrameworkCore; // Obavezno dodati za Include i FirstOrDefaultAsync
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<AddToCartCommand, int>
    {
        public async Task<int> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var userID = user.UserId;
            if (userID == null) throw new UnauthorizedAccessException("Korisnik nije prijavljen.");

            // FIXED (Item 27): Asynchronous call
            var CenterID = await ctx.Users
                .Where(x => x.UserID == userID)
                .Select(x => x.CenterID)
                .FirstOrDefaultAsync(cancellationToken);

            // FIXED (Items 27 and 28): Async call + Include for fetching existing items
            var cart = await ctx.Carts
                .Include(c => c.Items) 
                .Where(x => x.UserID == userID && !x.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (cart is null)
            {
                cart = new Domain.Entities.Store.Carts
                {
                    UserID = userID.Value,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    CenterID = CenterID,
                    Items = new List<Domain.Entities.Store.CartItems>(),
                    SubTotal = 0
                };
                await ctx.Carts.AddAsync(cart, cancellationToken);
            }
            
            // FIXED (Item 28): Check whether the plan already exists in cart
            var existingItem = cart.Items.FirstOrDefault(i => i.FitnessPlanID == request.FitnessPlanID && !i.IsDeleted);

            if (existingItem != null)
            {
                // If it exists, just increment quantity
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                // Ako ne postoji, kreiramo novi.
                // FIXED (Item 27): Async call for plan price
                var planPrice = await ctx.FitnessPlans
                            .Where(x => x.PlanID == request.FitnessPlanID)
                            .Select(x => (decimal)x.Price)
                            .FirstOrDefaultAsync(cancellationToken);

                var cartItem = new Domain.Entities.Store.CartItems
                {
                    FitnessPlanID = request.FitnessPlanID,
                    Quantity = request.Quantity,
                    CartID = cart.CartID,
                    IsDeleted = false,
                    Price = planPrice
                };

                cart.Items.Add(cartItem);
                await ctx.CartItems.AddAsync(cartItem, cancellationToken);
            }

            cart.SubTotal = cart.Items.Where(i => !i.IsDeleted).Sum(i => i.Price * i.Quantity);

            await ctx.SaveChangesAsync(cancellationToken);

            return cart.CartID;
        }
    }
}
