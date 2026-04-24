namespace FitHub.Application.Modules.Workout.Exercises.Queries.List
{
    public class GetExercisesQueryHandler(IAppDbContext ctx) : IRequestHandler<GetExercisesQuery, List<ExerciseDto>>
    {
        public async Task<List<ExerciseDto>> Handle(GetExercisesQuery request, CancellationToken ct)
        {
            var query = ctx.Exercises.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(e => e.Title.Contains(request.Search) || (e.Description != null && e.Description.Contains(request.Search)));
            }

            if (request.CenterID.HasValue)
            {
                query = query.Where(e => e.CenterID == request.CenterID.Value);
            }

            return await query
                .Select(e => new ExerciseDto
                {
                    ExerciseID = e.ExerciseID,
                    Title = e.Title,
                    Description = e.Description,
                    ImageURL = e.ImageURL,
                    VideoURL = e.VideoURL,
                    CenterID = e.CenterID,
                    UserID = e.UserID
                })
                .ToListAsync(ct);
        }
    }
}