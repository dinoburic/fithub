using FitHub.Domain.Entities.Blog;
using FitHub.Domain.Entities.Training;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Domain.Entities.Identity
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required int CenterID { get; set; }
        public FitnessCenters FitnessCenter { get; set; }
        public required bool Gender { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Status { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? FitnessPlanTypeID { get; set; }
        public FitnessPlanTypes FitnessPlanType { get; set; }
        public int RoleID { get; set; }
        public Roles Role { get; set; }
        public int? BadgeID { get; set; }
        public UserBadges? Badge { get; set; }
        public ICollection<Comments>? Comments { get; set; }
        public required bool IsDeleted { get; set; }
        public int? TokenVersion { get; set; } = 0;// For global revocation
        public ICollection<RefreshTokenEntity> RefreshTokens { get; private set; } = new List<RefreshTokenEntity>();
    }
}
