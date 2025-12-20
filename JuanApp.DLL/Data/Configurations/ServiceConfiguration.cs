using JuanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Advantage>
{
    public void Configure(EntityTypeBuilder<Advantage> builder)
    {
        builder.Property(s => s.Icon).IsRequired();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Description).IsRequired().HasMaxLength(500);
        builder.HasData(
          new Advantage
          {
              Id = 1,
              Name = "Web Development",
              Description = "Building responsive and robust web applications.",
              Icon = "policy-1.png"
          },
          new Advantage
          {
              Id = 2,
              Name = "Mobile App Development",
              Description = "Creating user-friendly mobile applications.",
              Icon = "policy-2.png"
          },
          new Advantage
          {
              Id = 3,
              Name = "Cloud Solutions",
              Description = "Scalable and secure cloud computing services.",
              Icon = "policy-3.png"
          }
      );
    }
}