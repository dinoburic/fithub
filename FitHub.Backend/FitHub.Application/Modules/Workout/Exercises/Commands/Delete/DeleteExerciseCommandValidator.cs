namespace FitHub.Application.Modules.Workout.Exercises.Commands.Delete
{
    public sealed class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
    {
        public DeleteExerciseCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}