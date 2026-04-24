namespace FitHub.Application.Modules.Workout.Exercises.Queries.GetById
{
    public class GetExerciseByIdQueryDto
    {
        public int ExerciseID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? VideoURL { get; set; }
        public int CenterID { get; set; }
        public string? Center { get; set; }
        public int UserID { get; set; }
        public string? Trainer { get; set; }
    }
}