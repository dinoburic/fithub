namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.GetById;

public sealed class GetFitnessPlanByIdQueryDto
{
    public int PlanID { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string? Difficulty { get; init; }
    public float Price { get; init; }
    public int? DailyDurationInMinutes { get; init; }
    public int? DurationInWeeks { get; init; }
    public int? ReviewsNumber { get; init; }
    public float? AverageRating { get; init; }
    public int CenterID { get; init; }
    public int FitnessPlanTypeID { get; init; }
    public int UserID { get; init; }
    public DateTime CreatedAtUtc { get; set; }
}
