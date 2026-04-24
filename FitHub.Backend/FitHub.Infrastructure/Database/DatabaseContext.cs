
using FitHub.Application.Abstractions;
using FitHub.Domain.Entities.Audit;
using FitHub.Domain.Entities.Blog;
using FitHub.Domain.Entities.Communication;
using FitHub.Domain.Entities.Store;
using FitHub.Domain.Entities.Training;

namespace FitHub.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<ProductCategoryEntity> ProductCategories => Set<ProductCategoryEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    public DbSet<BlogPosts> BlogPosts => Set<BlogPosts>();
    public DbSet<Categories> Categories => Set<Categories>();
    public DbSet<Comments> Comments => Set<Comments>();
    public DbSet<Reactions> Reactions => Set<Reactions>();

    public DbSet<Chats> Chats => Set<Chats>();
    public DbSet<ChatsUsers> ChatsUsers => Set<ChatsUsers>();
    public DbSet<Messages> Messages => Set<Messages>();
    public DbSet<Notifications> Notifications => Set<Notifications>();

    public DbSet<AuditLog> AuditLog => Set<AuditLog>();
    public DbSet<FitnessCenters> FitnessCenters => Set<FitnessCenters>();
    public DbSet<Roles> Roles => Set<Roles>();
    public DbSet<UserBadges> UserBadges => Set<UserBadges>();
    public DbSet<Users> Users => Set<Users>();

    public DbSet<CartItems> CartItems => Set<CartItems>();
    public DbSet<Carts> Carts => Set<Carts>();
    public DbSet<OrderItems> OrderItems => Set<OrderItems>();
    public DbSet<Orders> Orders => Set<Orders>();
    public DbSet<Payment> Payments => Set<Payment>();

   // public DbSet<Payments> Payments => Set<Payments>();
    public DbSet<WishListItems> WishListItems => Set<WishListItems>();
    public DbSet<WishLists> WishLists => Set<WishLists>();

    public DbSet<Enrollments> Enrollments => Set<Enrollments>();
    public DbSet<Exercises> Exercises => Set<Exercises>();
    public DbSet<FitnessPlans> FitnessPlans => Set<FitnessPlans>();
    public DbSet<FitnessPlanTypes> FitnessPlanTypes => Set<FitnessPlanTypes>();
    public DbSet<Ratings> Ratings => Set<Ratings>();
    public DbSet<Reviews> Reviews => Set<Reviews>();
    public DbSet<UsersFitnessPlanTypes> UsersFitnessPlanTypes => Set<UsersFitnessPlanTypes>();
    public DbSet<Workouts> Workouts => Set<Workouts>();



    private readonly TimeProvider _clock;
    public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider clock=null) : base(options)
    {
        _clock = clock ?? TimeProvider.System;
    }
}