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
                Description = "A timeless white t-shirt made from 100% cotton.",
                Price = 19.99m,
                ImageUrl = "product-1.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = true,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Blue Denim Jeans",
                Description = "Comfortable blue denim jeans with a modern fit.",
                Price = 49.99m,
                ImageUrl = "product-2.jpg",
                InStock = true,
                DiscountPercentage = 10,
                IsNew = false,
                IsMain = false,
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "Red Hoodie",
                Description = "A cozy red hoodie perfect for chilly days.",
                Price = 39.99m,
                ImageUrl = "product-3.jpg",
                InStock = true,
                DiscountPercentage = 5,
                IsNew = true,
                IsMain = false,
                CategoryId = 3
            },
            new Product
            {
                Id = 4,
                Name = "Black Leather Jacket",
                Description = "Stylish black leather jacket for a bold look.",
                Price = 99.99m,
                ImageUrl = "product-4.jpg",
                InStock = false,
                DiscountPercentage = 15,
                IsNew = false,
                IsMain = true,
                CategoryId = 4
            },
            new Product
            {
                Id = 5,
                Name = "Green Chinos",
                Description = "Casual green chinos made from breathable fabric.",
                Price = 44.99m,
                ImageUrl = "product-5.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 2
            },
            new Product
            {
                Id = 6,
                Name = "Yellow Summer Dress",
                Description = "Lightweight yellow dress perfect for summer outings.",
                Price = 59.99m,
                ImageUrl = "product-6.jpg",
                InStock = true,
                DiscountPercentage = 20,
                IsNew = false,
                IsMain = false,
                CategoryId = 5
            },
            new Product
            {
                Id = 7,
                Name = "Gray Sweatpants",
                Description = "Comfortable gray sweatpants for lounging or workouts.",
                Price = 29.99m,
                ImageUrl = "product-7.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 3
            },
            new Product
            {
                Id = 8,
                Name = "Navy Blue Blazer",
                Description = "Elegant navy blue blazer suitable for formal occasions.",
                Price = 79.99m,
                ImageUrl = "product-8.jpg",
                InStock = false,
                DiscountPercentage = 10,
                IsNew = false,
                IsMain = true,
                CategoryId = 4
            },
            new Product
            {
                Id = 9,
                Name = "Pink Floral Skirt",
                Description = "Charming pink skirt with floral patterns.",
                Price = 34.99m,
                ImageUrl = "product-9.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 5
            },
            new Product
            {
                Id = 10,
                Name = "Brown Ankle Boots",
                Description = "Durable brown ankle boots for everyday wear.",
                Price = 89.99m,
                ImageUrl = "product-10.jpg",
                InStock = true,
                DiscountPercentage = 15,
                IsNew = false,
                IsMain = false,
                CategoryId = 6
            },
            new Product
            {
                Id = 11,
                Name = "White Sneakers",
                Description = "Classic white sneakers that go with any outfit.",
                Price = 59.99m,
                ImageUrl = "product-11.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 6
            },
            new Product
            {
                Id = 12,
                Name = "Black Formal Shoes",
                Description = "Sleek black formal shoes for special occasions.",
                Price = 99.99m,
                ImageUrl = "product-12.jpg",
                InStock = false,
                DiscountPercentage = 20,
                IsNew = false,
                IsMain = true,
                CategoryId = 6
            },
            new Product
            {
                Id = 13,
                Name = "Orange Sports T-Shirt",
                Description = "Breathable orange t-shirt designed for sports activities.",
                Price = 24.99m,
                ImageUrl = "product-13.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 1
            },
            new Product
            {
                Id = 14,
                Name = "Green Running Shorts",
                Description = "Lightweight shorts perfect for running and workouts.",
                Price = 19.99m,
                ImageUrl = "product-14.jpg",
                InStock = true,
                DiscountPercentage = 5,
                IsNew = true,
                IsMain = false,
                CategoryId = 1
            },
            new Product
            {
                Id = 15,
                Name = "Black Fitness Gloves",
                Description = "Durable gloves for weightlifting and fitness training.",
                Price = 15.50m,
                ImageUrl = "product-15.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 2
            },
            new Product
            {
                Id = 16,
                Name = "Blue Yoga Mat",
                Description = "Non-slip yoga mat for indoor and outdoor exercises.",
                Price = 35.00m,
                ImageUrl = "product-16.jpg",
                InStock = true,
                DiscountPercentage = 10,
                IsNew = true,
                IsMain = false,
                CategoryId = 2
            },
            new Product
            {
                Id = 17,
                Name = "Red Gym Bag",
                Description = "Spacious gym bag with multiple compartments.",
                Price = 45.00m,
                ImageUrl = "product-17.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = false,
                IsMain = false,
                CategoryId = 3
            },
            new Product
            {
                Id = 18,
                Name = "White Sneakers",
                Description = "Comfortable sneakers suitable for daily wear.",
                Price = 75.00m,
                ImageUrl = "product-18.jpg",
                InStock = true,
                DiscountPercentage = 15,
                IsNew = true,
                IsMain = false,
                CategoryId = 3
            },
            new Product
            {
                Id = 19,
                Name = "Black Hoodie",
                Description = "Soft cotton hoodie with a minimalist design.",
                Price = 55.00m,
                ImageUrl = "product-19.jpg",
                InStock = true,
                DiscountPercentage = 5,
                IsNew = false,
                IsMain = false,
                CategoryId = 4
            },
            new Product
            {
                Id = 20,
                Name = "Gray Sweatpants",
                Description = "Comfortable sweatpants for lounging or workouts.",
                Price = 40.00m,
                ImageUrl = "product-20.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = false,
                IsMain = false,
                CategoryId = 4
            },
            new Product
            {
                Id = 21,
                Name = "Pink Sports Bra",
                Description = "Supportive sports bra for high-intensity workouts.",
                Price = 30.00m,
                ImageUrl = "product-21.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 5
            },
            new Product
            {
                Id = 22,
                Name = "Navy Running Shoes",
                Description = "Lightweight shoes designed for running efficiency.",
                Price = 85.00m,
                ImageUrl = "product-22.jpg",
                InStock = true,
                DiscountPercentage = 10,
                IsNew = true,
                IsMain = false,
                CategoryId = 5
            },
            new Product
            {
                Id = 23,
                Name = "Yellow Cap",
                Description = "Adjustable sports cap to keep the sun away.",
                Price = 12.99m,
                ImageUrl = "product-23.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 1
            },
            new Product
            {
                Id = 24,
                Name = "Purple Headband",
                Description = "Elastic headband for fitness and running.",
                Price = 7.99m,
                ImageUrl = "product-24.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 1
            },
            new Product
            {
                Id = 25,
                Name = "Orange Water Bottle",
                Description = "Reusable sports bottle with 1L capacity.",
                Price = 14.99m,
                ImageUrl = "product-25.jpg",
                InStock = true,
                DiscountPercentage = 0,
                IsNew = true,
                IsMain = false,
                CategoryId = 2
            }
        );
    }
}