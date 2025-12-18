using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuanApp.DLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "DiscountPercentage", "ImageUrl", "InStock", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "A comprehensive suite of productivity tools.", 10, "product-1.jpg", true, "Productivity Suite", 99.99m },
                    { 2, "Streamline your project workflows.", 15, "product-2.jpg", true, "Project Management Tool", 49.99m },
                    { 3, "Gain insights with our analytics platform.", 20, "product-3.jpg", false, "Analytics Platform", 149.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
