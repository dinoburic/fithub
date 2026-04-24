using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Blog
{
    public class BlogPosts
    {
        [Key]
        public int BlogPostID { get; set; } // PRIMARY KEY (identity)
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required string Title { get; set; }
        public required string? Content { get; set; }
        public required DateTime CreatedAtUTC { get; set; }
        public required DateTime? UpdatedAtUTC { get; set; }
        public required int CategoryID { get; set; }
        public required Categories Category { get; set; }
        public ICollection<Comments>? Comments { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
