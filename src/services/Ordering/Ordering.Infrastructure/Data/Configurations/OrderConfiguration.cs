using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .Property(o => o.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).IsRequired();

        builder.HasMany<OrderItem>().WithOne().HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName,
            nameBuilder =>
            {
                nameBuilder
                    .Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.ShippingAddress,
            nameBuilder =>
            {
                nameBuilder.Property(n => n.City).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.Country).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.State).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.Street).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.ZipCode).HasMaxLength(5).IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.BillingAddress,
            nameBuilder =>
            {
                nameBuilder.Property(n => n.City).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.Country).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.State).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.Street).HasMaxLength(50).IsRequired();
                nameBuilder.Property(n => n.ZipCode).HasMaxLength(5).IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.Payment,
            paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardNumber).HasMaxLength(16).IsRequired();
                paymentBuilder.Property(p => p.CardHolderName).HasMaxLength(100).IsRequired();
                paymentBuilder.Property(p => p.SecurityNumber).HasMaxLength(3).IsRequired();
                paymentBuilder.Property(p => p.Expiration).IsRequired();
            }
        );

        builder
            .Property(o => o.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                o => o.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
            );

        builder.Property(o => o.TotalPrice);
    }
}
