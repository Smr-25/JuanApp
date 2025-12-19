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
           Name = "Smart Watch Pro",
           Description = "Advanced smartwatch with health tracking features.",
           Price = 199.99m,
           ImageUrl = "product-1.jpg",
           InStock = true,
           DiscountPercentage = 10,
           IsNew = false,
           IsMain = true
       },
       new Product
       {
           Id = 2,
           Name = "Wireless Headphones",
           Description = "Noise cancelling over-ear wireless headphones.",
           Price = 149.50m,
           ImageUrl = "product-2.jpg",
           InStock = true,
           DiscountPercentage = 15,
           IsNew = false,
           IsMain = true
       },
       new Product
       {
           Id = 3,
           Name = "Gaming Keyboard",
           Description = "Mechanical keyboard with RGB backlight.",
           Price = 89.99m,
           ImageUrl = "product-3.jpg",
           InStock = true,
           DiscountPercentage = 0,
           IsNew = false,
           IsMain = true
       },

       new Product
       {
           Id = 4,
           Name = "Bluetooth Speaker",
           Description = "Portable speaker with deep bass and clear sound.",
           Price = 59.99m,
           ImageUrl = "product-4.jpg",
           InStock = true,
           DiscountPercentage = 5,
           IsNew = true,
           IsMain = false
       },
       new Product
       {
           Id = 5,
           Name = "Fitness Tracker",
           Description = "Lightweight tracker for daily activity monitoring.",
           Price = 49.99m,
           ImageUrl = "product-5.jpg",
           InStock = true,
           DiscountPercentage = 0,
           IsNew = true,
           IsMain = false
       },
       new Product
       {
           Id = 6,
           Name = "USB-C Hub",
           Description = "Multiport USB-C hub for modern laptops.",
           Price = 39.99m,
           ImageUrl = "product-6.jpg",
           InStock = true,
           DiscountPercentage = 20,
           IsNew = true,
           IsMain = false
       },
       new Product
       {
           Id = 7,
           Name = "4K Action Camera",
           Description = "Waterproof action camera for outdoor adventures.",
           Price = 129.99m,
           ImageUrl = "product-7.jpg",
           InStock = false,
           DiscountPercentage = 0,
           IsNew = true,
           IsMain = false
       },
       new Product
       {
           Id = 8,
           Name = "Wireless Mouse",
           Description = "Ergonomic wireless mouse with long battery life.",
           Price = 29.99m,
           ImageUrl = "product-8.jpg",
           InStock = true,
           DiscountPercentage = 10,
           IsNew = true,
           IsMain = false
       }
   );

    }
}