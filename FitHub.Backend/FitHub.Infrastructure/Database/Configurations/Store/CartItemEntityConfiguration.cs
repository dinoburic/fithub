using FitHub.Domain.Entities.Store;

namespace FitHub.Infrastructure.Database.Configurations.Store;

public sealed class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItems>
{
    public void Configure(EntityTypeBuilder<CartItems> b)
    {
        b.ToTable("CartItems");
        b.HasKey(x => x.CartItemID);

        b.HasIndex(x => x.CartID);
    }
}
