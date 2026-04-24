namespace FitHub.Application.Modules.Workout.Exercises.Commands.Update
{
    public sealed class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
    {
        public UpdateExerciseCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        }
    }
}