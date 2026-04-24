namespace FitHub.Infrastructure.Database.Configurations.Identity;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> b)
    {
        b.ToTable("Users");

        b.HasKey(x => x.UserID);

        b.HasIndex(x => x.Email)
            .IsUnique();

        b.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.PasswordHash)
            .IsRequired();

        // Roles
       
        b.Property(x => x.TokenVersion)
            .HasDefaultValue(0);

       

        // Navigation
        b.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}