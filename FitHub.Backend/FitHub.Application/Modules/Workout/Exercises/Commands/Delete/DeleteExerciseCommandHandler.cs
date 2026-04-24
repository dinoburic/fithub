namespace FitHub.Application.Modules.Workout.Exercises.Commands.Delete
{
    public sealed class DeleteExerciseCommandHandler(IAppDbContext ctx) : IRequestHandler<DeleteExerciseCommand,Unit>
    {
        public async Task<Unit> Handle(DeleteExerciseCommand request, CancellationToken ct)
        {
            var entity = await ctx.Exercises.FirstOrDefaultAsync(x => x.ExerciseID == request.Id, ct);
            if (entity is null)
                throw new MarketNotFoundException($"Exercise (ID={request.Id}) doesn't exist.");

            entity.IsDeleted = true;
            await ctx.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}