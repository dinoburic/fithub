
using FitHub.Application.Abstractions;
using FitHub.Domain.Entities.Audit;
using FitHub.Domain.Entities.Blog;
using FitHub.Domain.Entities.Communication;
using FitHub.Domain.Entities.Store;
using FitHub.Domain.Entities.Training;
namespace FitHub.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
    public DbSet<ProductEntity> Products { get; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; }

    public DbSet<BlogPosts> BlogPosts { get; }
    public DbSet<Categories> Categories { get; }
    public DbSet<Comments> Comments { get; }
    public DbSet<Reactions> Reactions { get; }

    public DbSet<Chats> Chats { get; }
    public DbSet<ChatsUsers> ChatsUsers { get; }
    public DbSet<Messages> Messages { get; }
    public DbSet<Notifications> Notifications { get; }

    public DbSet<AuditLog> AuditLog { get; }
    public DbSet<FitnessCenters> FitnessCenters { get; }
    public DbSet<Roles> Roles { get; }
    public DbSet<UserBadges> UserBadges { get; }
    public DbSet<Users> Users { get; }

    public DbSet<CartItems> CartItems { get; }
    public DbSet<Carts> Carts { get; }
    public DbSet<OrderItems> OrderItems { get; }
    public DbSet<Orders> Orders { get; }
    public DbSet<Payment> Payments {get;}
   // public DbSet<Payments> Payments { get; }
    public DbSet<WishListItems> WishListItems { get; }
    public DbSet<WishLists> WishLists { get; }

    public DbSet<Enrollments> Enrollments { get; }
    public DbSet<Exercises> Exercises { get; }
    public DbSet<FitnessPlans> FitnessPlans { get; }
    public DbSet<FitnessPlanTypes> FitnessPlanTypes { get; }
    public DbSet<Ratings> Ratings { get; }
    public DbSet<Reviews> Reviews { get; }
    public DbSet<UsersFitnessPlanTypes> UsersFitnessPlanTypes { get; }
    public DbSet<Workouts> Workouts { get; }



    Task<int> SaveChangesAsync(CancellationToken ct);
}