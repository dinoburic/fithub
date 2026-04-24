namespace FitHub.Application.Modules.Workout.FitnessPlans.Commands.Update;

public sealed class UpdateFitnessPlanCommandValidator : AbstractValidator<UpdateFitnessPlanCommand>
{
    public UpdateFitnessPlanCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}
