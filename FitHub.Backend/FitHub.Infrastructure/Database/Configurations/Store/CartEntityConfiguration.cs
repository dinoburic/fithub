using FitHub.Domain.Entities.Store;

namespace FitHub.Infrastructure.Database.Configurations.Store;

public sealed class CartEntityConfiguration : IEntityTypeConfiguration<Carts>
{
    public void Configure(EntityTypeBuilder<Carts> b)
    {
        b.ToTable("Carts");
        b.HasKey(x => x.CartID);

        b.HasIndex(x => x.UserID);
    }
}
