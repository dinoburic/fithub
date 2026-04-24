using FitHub.Domain.Entities.Training;
using FitHub.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitHub.Infrastructure.Database.Configurations.Training;

public sealed class FitnessPlanEntityConfiguration : IEntityTypeConfiguration<FitnessPlans>
{
    public void Configure(EntityTypeBuilder<FitnessPlans> b)
    {
        b.ToTable("FitnessPlans");

        b.HasKey(x => x.PlanID);

        b.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Description)
            .HasMaxLength(2000);

        b.Property(x => x.Difficulty)
            .HasMaxLength(100);

        b.Property(x => x.Price)
            .IsRequired();

        b.Property(x => x.AverageRating)
            .HasDefaultValue(0f);

        b.Property(x => x.ReviewsNumber)
            .HasDefaultValue(0);

        // Foreign keys / navigation
        b.HasOne(x => x.Center)
            .WithMany()
            .HasForeignKey(x => x.CenterID)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.FitnessPlanType)
            .WithMany()
            .HasForeignKey(x => x.FitnessPlanTypeID)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
