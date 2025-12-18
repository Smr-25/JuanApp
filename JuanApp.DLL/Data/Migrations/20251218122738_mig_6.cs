using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuanApp.DLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "Address", "1234 Street Name, City, Country" },
                    { "ContactEmail", "myemail@gmail.com" },
                    { "ContactPhone", "+ 00 123 254565" },
                    { "FacebookUrl", "https://facebook.com/yourpage" },
                    { "InstagramUrl", "https://instagram.com/yourprofile" },
                    { "LinkedInUrl", "https://linkedin.com/in/yourprofile" },
                    { "Logo", "logo.png" },
                    { "TwitterUrl", "https://twitter.com/yourprofile" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
