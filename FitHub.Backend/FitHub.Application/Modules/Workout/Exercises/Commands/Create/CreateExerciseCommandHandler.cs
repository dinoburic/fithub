using FitHub.Application.Abstractions;
using MediatR;
using FitHub.Domain.Entities.Training;
using ValidationException = FluentValidation.ValidationException;

namespace FitHub.Application.Modules.Workout.Exercises.Commands.Create
{
    public class CreateExerciseCommandHandler(IAppDbContext ctx) : IRequestHandler<CreateExerciseCommand, int>
    {
        public async Task<int> Handle(CreateExerciseCommand request, CancellationToken ct)
        {
            var title = request.Title?.Trim();
            if (string.IsNullOrWhiteSpace(title))
                throw new ValidationException("Title is required.");

            var entity = new Domain.Entities.Training.Exercises
            {
                Title = title,
                Description = request.Description,
                ImageURL = request.ImageURL,
                VideoURL = request.VideoURL,
                CenterID = request.CenterID,
                UserID = request.UserID,
                IsDeleted = false
            };

            ctx.Exercises.Add(entity);
            await ctx.SaveChangesAsync(ct);

            return entity.ExerciseID;
        }
    }
}