using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.Property(a => a.ProductId).IsRequired();
        builder.Property(a => a.ImageUrl).IsRequired().HasMaxLength(255);
        builder.HasData(
            new ProductImage { Id = 1, ProductId = 1, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 2, ProductId = 1, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 3, ProductId = 1, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 4, ProductId = 1, ImageUrl = "product-details-img4.jpg" },
            new ProductImage { Id = 5, ProductId = 2, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 6, ProductId = 2, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 7, ProductId = 2, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 8, ProductId = 2, ImageUrl = "product-details-img4.jpg" },
            new ProductImage { Id = 9, ProductId = 3, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 10, ProductId = 3, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 11, ProductId = 3, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 12, ProductId = 3, ImageUrl = "product-details-img4.jpg" },
            new ProductImage { Id = 13, ProductId = 4, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 14, ProductId = 4, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 15, ProductId = 4, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 16, ProductId = 4, ImageUrl = "product-details-img4.jpg" },
            new ProductImage { Id = 17, ProductId = 5, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 18, ProductId = 5, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 19, ProductId = 5, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 20, ProductId = 5, ImageUrl = "product-details-img4.jpg" } ,
            new ProductImage { Id = 21, ProductId = 6, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 22, ProductId = 6, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 23, ProductId = 6, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 24, ProductId = 6, ImageUrl = "product-details-img4.jpg" },
            new ProductImage { Id = 25, ProductId = 7, ImageUrl = "product-details-img1.jpg" }, 
            new ProductImage { Id = 26, ProductId = 7, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 27, ProductId = 7, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 28, ProductId = 7, ImageUrl = "product-details-img4.jpg" },
            new ProductImage { Id = 29, ProductId = 8, ImageUrl = "product-details-img1.jpg" },
            new ProductImage { Id = 30, ProductId = 8, ImageUrl = "product-details-img2.jpg" },
            new ProductImage { Id = 31, ProductId = 8, ImageUrl = "product-details-img3.jpg" },
            new ProductImage { Id = 32, ProductId = 8, ImageUrl = "product-details-img4.jpg" }
        );
    }
}