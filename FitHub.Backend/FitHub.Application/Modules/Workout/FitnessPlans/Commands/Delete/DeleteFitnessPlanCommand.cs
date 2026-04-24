using MediatR;

namespace FitHub.Application.Modules.Training.FitnessPlans.Commands.Delete;

public sealed class DeleteFitnessPlanCommand : IRequest
{
    public int Id { get; set; }
}
