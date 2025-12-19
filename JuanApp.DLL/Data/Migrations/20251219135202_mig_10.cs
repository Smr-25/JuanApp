using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuanApp.DLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "IsMain", "Name", "Price" },
                values: new object[] { "Advanced smartwatch with health tracking features.", "product-1.png", true, "Smart Watch Pro", 199.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "IsMain", "Name", "Price" },
                values: new object[] { "Noise cancelling over-ear wireless headphones.", "product-2.png", true, "Wireless Headphones", 149.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "DiscountPercentage", "ImageUrl", "InStock", "IsMain", "Name", "Price" },
                values: new object[] { "Mechanical keyboard with RGB backlight.", 0, "product-3.png", true, true, "Gaming Keyboard", 89.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "DiscountPercentage", "ImageUrl", "InStock", "IsMain", "IsNew", "Name", "Price" },
                values: new object[,]
                {
                    { 4, "Portable speaker with deep bass and clear sound.", 5, "product-4.png", true, false, true, "Bluetooth Speaker", 59.99m },
                    { 5, "Lightweight tracker for daily activity monitoring.", 0, "product-5.png", true, false, true, "Fitness Tracker", 49.99m },
                    { 6, "Multiport USB-C hub for modern laptops.", 20, "product-6.png", true, false, true, "USB-C Hub", 39.99m },
                    { 7, "Waterproof action camera for outdoor adventures.", 0, "product-7.png", false, false, true, "4K Action Camera", 129.99m },
                    { 8, "Ergonomic wireless mouse with long battery life.", 10, "product-8.png", true, false, true, "Wireless Mouse", 29.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "IsMain", "Name", "Price" },
                values: new object[] { "A comprehensive suite of productivity tools.", "product-1.jpg", false, "Productivity Suite", 99.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "IsMain", "Name", "Price" },
                values: new object[] { "Streamline your project workflows.", "product-2.jpg", false, "Project Management Tool", 49.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "DiscountPercentage", "ImageUrl", "InStock", "IsMain", "Name", "Price" },
                values: new object[] { "Gain insights with our analytics platform.", 20, "product-3.jpg", false, false, "Analytics Platform", 149.99m });
        }
    }
}
