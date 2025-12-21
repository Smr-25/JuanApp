using JuanApp.Core.Models;
using JuanApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.DLL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Advantage> Advantages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Setting> Settings { get; set; }
    
    public DbSet<ProductImage> ProductImages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}