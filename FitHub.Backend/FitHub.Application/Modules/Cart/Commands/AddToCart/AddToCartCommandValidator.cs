using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitHub.Application.Modules.Cart.Commands.AddToCart
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        private readonly IAppDbContext _ctx;

        public AddToCartCommandValidator(IAppDbContext ctx)
        {
            _ctx = ctx;

            RuleFor(x => x.FitnessPlanID)
                .GreaterThan(0).WithMessage("FitnessPlanID must be greater than 0.");
            RuleFor(x => x.FitnessPlanID)
                .NotEmpty().WithMessage("FitnessPlanID is required.")
                .MustAsync(FitnessPlanExists).WithMessage("Fitness plan does not exist");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }

        private async Task<bool> FitnessPlanExists(int fitnessPlanId, CancellationToken cancellationToken)
        {
            return await _ctx.FitnessPlans.AnyAsync(u => u.PlanID == fitnessPlanId && !u.IsDeleted, cancellationToken);
        }
    }
}
