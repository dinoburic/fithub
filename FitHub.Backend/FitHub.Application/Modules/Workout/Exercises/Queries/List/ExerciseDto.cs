namespace FitHub.Application.Modules.Workout.Exercises.Queries.List
{
    public class ExerciseDto
    {
        public int ExerciseID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? VideoURL { get; set; }
        public int CenterID { get; set; }
        public int UserID { get; set; }
    }
}