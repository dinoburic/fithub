namespace FitHub.Application.Modules.Workout.Exercises.Commands.Update
{
    public sealed class UpdateExerciseCommandHandler(IAppDbContext ctx) : IRequestHandler<UpdateExerciseCommand>
    {
        async Task IRequestHandler<UpdateExerciseCommand>.Handle(UpdateExerciseCommand request, CancellationToken ct)
        {
            var entity = await ctx.Exercises.FirstOrDefaultAsync(x => x.ExerciseID == request.Id, ct);
            if (entity is null)
                throw new MarketNotFoundException($"Exercise (ID={request.Id}) doesn't exist.");

            entity.Title = request.Title.Trim();
            entity.Description = request.Description;
            entity.ImageURL = request.ImageURL;
            entity.VideoURL = request.VideoURL;

            await ctx.SaveChangesAsync(ct);
        }
    }
}