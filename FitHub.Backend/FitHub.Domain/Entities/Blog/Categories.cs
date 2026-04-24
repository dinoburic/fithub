using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Blog
{
    public class Categories
    {
        [Key]
        public required int CategoryID { get; set; } // PRIMARY KEY
        public required string Title { get; set; }
        public required string? Description { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
