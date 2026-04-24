using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Queries.GetById
{
    public class GetReviewByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetReviewByIdQuery, GetReviewByIdQueryDto>
    {
        public async Task<GetReviewByIdQueryDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await ctx.Reviews
                .AsNoTracking()
                .Include(x => x.FitnessPlan)
                .Include(x => x.User)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.ReviewID == request.Id, cancellationToken);

            if (review == null)
            {
                throw new KeyNotFoundException("Recenzija nije pronađena.");
            }

            return new GetReviewByIdQueryDto
            {
                ReviewID = review.ReviewID,
                Rating = review.Rating,
                Comment = review.Comment,
                FitnessPlanID = review.FitnessPlanID,
                PlanTitle = review.FitnessPlan?.Title ?? "Nepoznat plan",
                UserID = review.UserID,
                UserName = $"{review.User?.Name}",
                CreatedAtUTC = review.CreatedAtUTC
            };
        }
    }

    public sealed class GetReviewByIdQueryValidator : AbstractValidator<GetReviewByIdQuery>
    {
        public GetReviewByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Review ID mora biti veći od nule.");
        }
    }
}
