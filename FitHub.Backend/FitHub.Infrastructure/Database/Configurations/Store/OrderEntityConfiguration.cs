using FitHub.Domain.Entities.Store;

namespace FitHub.Infrastructure.Database.Configurations.Store;

public sealed class OrderEntityConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> b)
    {
        b.ToTable("Orders");
        b.HasKey(x => x.OrderID);

        b.HasIndex(x => x.UserID);
        b.HasIndex(x => x.Status);
    }
}
