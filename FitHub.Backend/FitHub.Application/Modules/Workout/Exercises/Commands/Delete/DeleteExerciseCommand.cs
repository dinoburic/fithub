namespace FitHub.Application.Modules.Workout.Exercises.Commands.Delete
{
    public class DeleteExerciseCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}