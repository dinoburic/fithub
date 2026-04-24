using FluentValidation;

namespace FitHub.Application.Modules.Training.FitnessPlans.Commands.Delete;

public sealed class DeleteFitnessPlanCommandValidator : AbstractValidator<DeleteFitnessPlanCommand>
{
    public DeleteFitnessPlanCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
