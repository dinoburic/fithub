using System.ComponentModel.DataAnnotations;
using MediatR;

namespace FitHub.Application.Modules.Workout.Exercises.Commands.Create
{
    public class CreateExerciseCommand : IRequest<int>
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? VideoURL { get; set; }
        [Required]
        public int CenterID { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}
