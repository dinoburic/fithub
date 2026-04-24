using MediatR;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.GetById;

public sealed class GetFitnessPlanByIdQuery : IRequest<GetFitnessPlanByIdQueryDto>
{
    public int Id { get; set; }
}
