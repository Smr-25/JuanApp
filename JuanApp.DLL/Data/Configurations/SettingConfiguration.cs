using JuanApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuanApp.DLL.Data.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasKey(s => s.Key);

        builder.Property(s => s.Key)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.Value)
               .IsRequired()
               .HasMaxLength(500);


        builder.HasData(
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
