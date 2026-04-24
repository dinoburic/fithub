using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Update
{
    public sealed class UpdateReviewCommandHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<UpdateReviewCommand>
    {
        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await ctx.Reviews
                .FirstOrDefaultAsync(x => x.ReviewID == request.ReviewID && !x.IsDeleted, cancellationToken);

            if (review == null)
            {
                throw new KeyNotFoundException($"Recenzija sa ID-em {request.ReviewID} ne postoji.");
            }

            if (review.UserID != user.UserId)
            {
                throw new UnauthorizedAccessException("Možete mijenjati samo svoje recenzije.");
            }

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.UpdatedAtUTC = DateTime.UtcNow;

            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
