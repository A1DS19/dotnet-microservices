using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountContext(DbContextOptions<DiscountContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Coupon>()
            .HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "IPhone X",
                    Description = "IPhone Discount",
                    Amount = 150,
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Samsung 10",
                    Description = "Samsung Discount",
                    Amount = 100,
                },
                new Coupon
                {
                    Id = 3,
                    ProductName = "Huawei P30",
                    Description = "Huawei Discount",
                    Amount = 50,
                },
                new Coupon
                {
                    Id = 4,
                    ProductName = "Xiaomi Mi 10",
                    Description = "Xiaomi Discount",
                    Amount = 20,
                },
                new Coupon
                {
                    Id = 5,
                    ProductName = "OnePlus 7T",
                    Description = "OnePlus Discount",
                    Amount = 80,
                }
            );
    }
}
