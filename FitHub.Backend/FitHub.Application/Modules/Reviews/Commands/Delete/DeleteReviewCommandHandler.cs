using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Delete
{
    public class DeleteReviewCommandHandler(IAppDbContext ctx, IAppCurrentUser user) : IRequestHandler<DeleteReviewCommand>
    {
        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await ctx.Reviews
                .FirstOrDefaultAsync(x => x.ReviewID == request.ReviewID, cancellationToken);

            if (review == null)
            {
                throw new KeyNotFoundException($"Recenzija sa ID-em {request.ReviewID} ne postoji.");
            }

            if (review.IsDeleted)
            {
                throw new MarketNotFoundException("Ova recenzija je već obrisana.");
            }

            if (review.UserID != user.UserId)
            {
                throw new UnauthorizedAccessException("Možete brisati samo svoje recenzije.");
            }

            review.IsDeleted = true;
            review.UpdatedAtUTC = DateTime.UtcNow;

            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
