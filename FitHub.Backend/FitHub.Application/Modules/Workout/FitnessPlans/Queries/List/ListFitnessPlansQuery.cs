using FitHub.Application.Common;
using MediatR;

namespace FitHub.Application.Modules.Workout.FitnessPlans.Queries.List;

public sealed class ListFitnessPlansQuery : BasePagedQuery<ListFitnessPlansQueryDto>
{
    public string? Search { get; set; }
    public int? CenterID { get; set; }
    public int? FitnessPlanTypeID { get; set; }
    public string? Difficulty { get; set; }
    public float? MinPrice { get; set; }
    public float? MaxPrice { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public string? OrderBy { get; set; }
}
