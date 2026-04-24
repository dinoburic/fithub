namespace FitHub.Application.Modules.Workout.Exercises.Commands.Create
{
    public sealed class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CenterID).GreaterThan(0);
            RuleFor(x => x.UserID).GreaterThan(0);
        }
    }
}