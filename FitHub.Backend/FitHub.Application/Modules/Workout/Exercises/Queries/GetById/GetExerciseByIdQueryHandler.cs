using FitHub.Application.Abstractions;
using FitHub.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Application.Modules.Workout.Exercises.Queries.GetById
{
    public sealed class GetExerciseByIdQueryHandler(IAppDbContext ctx) : IRequestHandler<GetExerciseByIdQuery, GetExerciseByIdQueryDto>
    {
        public async Task<GetExerciseByIdQueryDto> Handle(GetExerciseByIdQuery request, CancellationToken ct)
        {
            var exercise = await ctx.Exercises
                .Where(e => e.ExerciseID == request.Id)
                .Select(e => new GetExerciseByIdQueryDto
                {
                    ExerciseID = e.ExerciseID,
                    Title = e.Title,
                    Description = e.Description,
                    ImageURL = e.ImageURL,
                    VideoURL = e.VideoURL,
                    CenterID = e.CenterID,
                    Center = e.Center != null ? e.Center.Name : null,
                    UserID = e.UserID,
                    Trainer = e.User != null ? e.User.Name + " " + e.User.Surname : null
                })
                .FirstOrDefaultAsync(ct);

            if (exercise is null)
                throw new MarketNotFoundException($"Exercise (ID={request.Id}) doesn't exist.");

            return exercise;
        }
    }
}