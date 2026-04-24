using FitHub.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Domain.Entities.Blog
{
    public class Reactions
    {
        [Key]
        public required int ReactionID { get; set; } // PRIMARY KEY
        public required int UserID { get; set; }
        public required Users User { get; set; }
        public required int BlogPostID { get; set; }
        public required BlogPosts BlogPost { get; set; }
        public required string Type { get; set; }
        public required DateTime DateTimeAddedUTC { get; set; }
        public required bool IsDeleted { get; set; }
    }
}
