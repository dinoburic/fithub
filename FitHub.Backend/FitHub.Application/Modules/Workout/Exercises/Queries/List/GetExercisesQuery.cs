using FitHub.Application.Common;
using MediatR;

namespace FitHub.Application.Modules.Workout.Exercises.Queries.List
{
    public sealed class GetExercisesQuery : IRequest<List<ExerciseDto>>
    {
        public string? Search { get; set; }
        public int? CenterID { get; set; }
    }
}