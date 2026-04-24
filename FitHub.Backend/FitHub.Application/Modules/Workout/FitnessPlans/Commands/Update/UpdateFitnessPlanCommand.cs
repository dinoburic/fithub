using MediatR;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Commands.Update;

public sealed class UpdateFitnessPlanCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Difficulty { get; set; }
    public required float Price { get; set; }
    public int? DailyDurationInMinutes { get; set; }
    public int? DurationInWeeks { get; set; }
    public int? ReviewsNumber { get; set; }
    public float? AverageRating { get; set; }
}
