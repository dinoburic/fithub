using FitHub.Domain.Entities.Store;

namespace FitHub.Infrastructure.Database.Configurations.Store;

public sealed class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItems>
{
    public void Configure(EntityTypeBuilder<OrderItems> b)
    {
        b.ToTable("OrderItems");
        b.HasKey(x => x.OrderItemID);

        b.HasIndex(x => x.OrderID);
    }
}
