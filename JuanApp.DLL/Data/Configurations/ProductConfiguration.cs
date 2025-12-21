using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace JuanApp.DLL.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.Property(p => p.InStock).IsRequired();
        builder.Property(p => p.DiscountPercentage).IsRequired();
        builder.Property(p => p.IsNew).IsRequired();
        builder.Property(p => p.IsMain).IsRequired();
        builder.HasData(
            new Product
            {
                Id = 1,
                Name = "Classic White T-Shirt",
                Description = "100% cotton, everyday essential t-shirt.",
                Price = 25,
                ImageUrl = "product-1.jpg",
                InStock = true,
                DiscountPercentage = 10,
                IsNew = true,
                IsMain = true,
                CategoryId = 1,
            },
            new Product
            {
                Id = 2,
                Name = "Slim Fit Jeans",
                Description = "Modern slim fit jeans with stretch fabric.",
                Price = 80,
                ImageUrl = "product-2.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = false,
                IsMain = true,
                CategoryId = 2,
            },
            new Product
            {
                Id = 3,
                Name = "Leather Jacket",
                Description = "Premium leather jacket for all seasons.",
                Price = 220,
                ImageUrl = "product-3.jpg",
                InStock = false,
                DiscountPercentage = 15,
                IsNew = false,
                IsMain = true,
                CategoryId = 3,
            },
            new Product
            {
                Id = 4,
                Name = "Summer Dress",
                Description = "Lightweight floral summer dress.",
                Price = 65,
                ImageUrl = "product-4.jpg",
                InStock = true,
                DiscountPercentage = 20,
                IsNew = true,
                IsMain = false,
                CategoryId = 4,
                
            },
            new Product
            {
                Id = 5,
                Name = "Sport Sneakers",
                Description = "Comfortable sneakers for daily use.",
                Price = 110,
                ImageUrl = "product-5.jpg",
                InStock = true,
                DiscountPercentage = 5,
                IsNew = true,
                IsMain = false,
                CategoryId = 5,
            },
            new Product
            {
                Id = 6,
                Name = "Hoodie",
                Description = "Warm hoodie with minimalist design.",
                Price = 55,
                ImageUrl = "product-6.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = false,
                IsMain = false,
                CategoryId = 1,
            },
            new Product
            {
                Id = 7,
                Name = "Formal Shirt",
                Description = "Perfect shirt for office and events.",
                Price = 70,
                ImageUrl = "product-7.jpg",
                InStock = true,
                DiscountPercentage = 10,
                IsNew = false,
                IsMain = false,
                CategoryId = 2,
            },
            new Product
            {
                Id = 8,
                Name = "Winter Coat",
                Description = "Heavy winter coat with insulation.",
                Price = 180,
                ImageUrl = "product-8.jpg",
                InStock = false,
                DiscountPercentage = 25,
                IsNew = true,
                IsMain = false,
                CategoryId = 3,
            }
        );
    }
}