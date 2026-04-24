using System.Text.Json.Serialization;
using MediatR;

namespace FitHub.Application.Modules.Workout.Exercises.Commands.Update
{
    public sealed class UpdateExerciseCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? VideoURL { get; set; }
    }
}