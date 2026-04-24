using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Blog
{
    public class Comments
    {
        [Key]
        public required int CommentID { get; set; }
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required int BlogPostID { get; set; }
        public required BlogPosts BlogPost { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAtUTC { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
