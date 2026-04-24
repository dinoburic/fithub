using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Create
{
    public sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Max length is 500 characters.");

            RuleFor(x => x.FitnessPlanID)
                .GreaterThan(0).WithMessage("FitnessPlanID is required.");

        }
    }
}
