using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.DLL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Product> Products { get; set; }

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
                ButtonLink = "/about"

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
        
    }
    
}
