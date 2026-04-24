using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Reviews.Commands.Update
{
    public sealed class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.ReviewID)
                .GreaterThan(0).WithMessage("ReviewID mora biti veći od 0.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Ocjena mora biti između 1 i 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Komentar ne može biti duži od 500 karaktera.");
        }
    }
}
