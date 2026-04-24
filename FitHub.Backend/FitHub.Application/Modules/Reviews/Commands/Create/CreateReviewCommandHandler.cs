using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Needed for AnyAsync

namespace FitHub.Application.Modules.Reviews.Commands.Create
{
    public sealed class CreateReviewCommandHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<CreateReviewCommand, int>
    {
        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = user.UserId ?? throw new UnauthorizedAccessException("User not logged in.");
            
            // FIXED (Item 29): Removed fallback value 1
            var centerId = user.CenterId ?? throw new InvalidOperationException("Nije moguće utvrditi pripadnost fitness centru. CenterID nedostaje u tokenu.");

            bool hasPurchased = await ctx.Orders
                                        .Where(o => o.UserID == userId && !o.IsDeleted)
                                        .SelectMany(o => o.Items) 
                                        .AnyAsync(i => i.FitnessPlanID == request.FitnessPlanID, cancellationToken);

            if (!hasPurchased)
            {
                throw new InvalidOperationException("You have to buy the plan in order to review it.");
            }

            var review = new Domain.Entities.Training.Reviews
            {
                Rating = request.Rating,
                Comment = request.Comment,
                FitnessPlanID = request.FitnessPlanID,
                
                // FIXED: Now we use validated centerId without "?? 1"
                CenterID = centerId,
                
                UserID = userId,
                CreatedAtUTC = DateTime.UtcNow,
                IsDeleted = false
            };

           
            
            ctx.Reviews.Add(review);
            await ctx.SaveChangesAsync(cancellationToken);

            return review.ReviewID;
        }
    }
}
