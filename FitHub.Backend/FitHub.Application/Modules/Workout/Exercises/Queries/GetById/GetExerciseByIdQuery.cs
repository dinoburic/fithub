using FitHub.Application.Modules.Workout.Exercises.Queries.List;
using MediatR;

namespace FitHub.Application.Modules.Workout.Exercises.Queries.GetById
{
    public class GetExerciseByIdQuery : IRequest<GetExerciseByIdQueryDto>
    {
        public int Id { get; set; }
    }
}