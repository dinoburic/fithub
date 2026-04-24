using FitHub.Domain.Common;
using FitHub.Domain.Entities.Audit;
using FitHub.Domain.Entities.Blog;
using FitHub.Domain.Entities.Store;
using FitHub.Infrastructure.Database.Configurations.Store;
using FitHub.Infrastructure.Database.Seeders;
using System.Linq.Expressions;

namespace FitHub.Infrastructure.Database;

public partial class DatabaseContext
{
    private DateTime UtcNow => _clock.GetUtcNow().UtcDateTime;

    private void ApplyAuditAndSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = UtcNow;
                    entry.Entity.ModifiedAtUtc = null; // ili = UtcNow
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedAtUtc = UtcNow;
                    break;

                case EntityState.Deleted:
                    // soft-delete: set is Modified and IsDeleted
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.ModifiedAtUtc = UtcNow;
                    break;
            }
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        configurationBuilder.Properties<decimal?>().HavePrecision(18, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CartEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemEntityConfiguration());

        ApplyGlobalFielters(modelBuilder);

        StaticDataSeeder.Seed(modelBuilder); // static data

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }

        modelBuilder.Entity<AuditLog>()
       .HasOne(a => a.User)
       .WithMany()
       .HasForeignKey(a => a.UserID)
       .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CartItems>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AuditLog>()
            .HasOne(a => a.FitnessCenter)
            .WithMany()
            .HasForeignKey(a => a.CenterID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Users>()
            .HasOne(u => u.FitnessCenter)
            .WithMany()
            .HasForeignKey(u => u.CenterID)
            .OnDelete(DeleteBehavior.NoAction);

        //modelBuilder.Entity<Comments>()
        //    .HasOne(c => c.User)
        //    .WithMany()
        //    .HasForeignKey(c => c.UserID)
        //    .OnDelete(DeleteBehavior.NoAction);

        //modelBuilder.Entity<Comments>()
        //  .HasOne(c => c.BlogPost)
        //  .WithMany()
        //  .HasForeignKey(c => c.BlogPostID)
        //  .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Users>()
        .Property(u => u.UserID)
        .ValueGeneratedOnAdd();


        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(c => c.CommentID);
            entity.Property(c => c.CommentID)
                  .ValueGeneratedOnAdd(); // IDENTITY

            entity.HasOne(c => c.User)
                  .WithMany(u => u.Comments) // pretpostavimo da Users ima ICollection<Comments>
                  .HasForeignKey(c => c.UserID)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.BlogPost)
                  .WithMany(b => b.Comments) // BlogPosts ima ICollection<Comments>
                  .HasForeignKey(c => c.BlogPostID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

    }

    private void ApplyGlobalFielters(ModelBuilder modelBuilder)
    {
        // Apply a global filter to all entities inheriting from BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var compare = Expression.Equal(prop, Expression.Constant(false));
                var lambda = Expression.Lambda(compare, parameter);

                modelBuilder.Entity(entityType.ClrType)
                            .HasQueryFilter(lambda);
            }
        }
    }

    public override int SaveChanges()
    {
        ApplyAuditAndSoftDelete();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyAuditAndSoftDelete();

        return base.SaveChangesAsync(cancellationToken);
    }
}
