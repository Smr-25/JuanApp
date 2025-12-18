using JuanApp.Core.Models;
using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.DLL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Setting> Settings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<Slider>().HasData(
            new Slider
            {
                Id = 1,
                Title = "Welcome to JuanApp",
                SubTitle = "Quality Software Solutions",
                Description = "Your trusted partner in software solutions.",
                ImageUrl = "slider-1.jpg",
                ButtonText = "Learn More",
                ButtonLink = "/about",
            },
            new Slider
            {
                Id = 2,
                Title = "Innovative Solutions",
                SubTitle = "Cutting-Edge Technology",
                Description = "Transforming ideas into reality.",
                ImageUrl = "slider-2.jpg",
                ButtonText = "Our Services",
                ButtonLink = "/services"
            }
        );

        modelBuilder.Entity<Service>().HasData(
            new Service
            {
                Id = 1,
                Name = "Web Development",
                Description = "Building responsive and robust web applications.",
                Icon = "policy-1.png"
            },
            new Service
            {
                Id = 2,
                Name = "Mobile App Development",
                Description = "Creating user-friendly mobile applications.",
                Icon = "policy-2.png"
            },
            new Service
            {
                Id = 3,
                Name = "Cloud Solutions",
                Description = "Scalable and secure cloud computing services.",
                Icon = "policy-3.png"
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Productivity Suite",
                Description = "A comprehensive suite of productivity tools.",
                Price = 99.99m,
                ImageUrl = "product-1.jpg",
                InStock = true,
                DiscountPercentage = 10
            },
            new Product
            {
                Id = 2,
                Name = "Project Management Tool",
                Description = "Streamline your project workflows.",
                Price = 49.99m,
                ImageUrl = "product-2.jpg",
                InStock = true,
                DiscountPercentage = 15
            },
            new Product
            {
                Id = 3,
                Name = "Analytics Platform",
                Description = "Gain insights with our analytics platform.",
                Price = 149.99m,
                ImageUrl = "product-3.jpg",
                InStock = false,
                DiscountPercentage = 20
            }
        );
        modelBuilder.Entity<Setting>().HasData(
            new Setting
            { 
                Key = "Logo",
                Value = "logo.png"
            },
            new Setting
            {
                Key = "ContactEmail",
                Value = "myemail@gmail.com"
            },
            new Setting
            {

                Key = "ContactPhone",
                Value = "+ 00 123 254565"
            },
            new Setting
            {
                Key = "Address",
                Value = "1234 Street Name, City, Country"
            },
            new Setting
            {
                Key = "FacebookUrl",
                Value = "https://facebook.com/yourpage"
            },
            new Setting
            {
                Key = "TwitterUrl",
                Value = "https://twitter.com/yourprofile"
            },
            new Setting
            {
                Key = "LinkedInUrl",
                Value = "https://linkedin.com/in/yourprofile"
            },
            new Setting
            {
                Key = "InstagramUrl",
                Value = "https://instagram.com/yourprofile"
            }
            );
    }
}