using FluentValidation;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Commands.Create;

public sealed class CreateFitnessPlanCommandValidator : AbstractValidator<CreateFitnessPlanCommand>
{
    public CreateFitnessPlanCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CenterID).GreaterThan(0);
        RuleFor(x => x.FitnessPlanTypeID).GreaterThan(0);
        RuleFor(x => x.UserID).GreaterThan(0);
    }
}
