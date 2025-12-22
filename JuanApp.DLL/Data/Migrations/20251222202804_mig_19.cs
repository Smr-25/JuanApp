using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuanApp.DLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DiscountPercentage", "Price" },
                values: new object[] { "A timeless white t-shirt made from 100% cotton.", 0, 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "DiscountPercentage", "IsMain", "Name", "Price" },
                values: new object[] { "Comfortable blue denim jeans with a modern fit.", 10, false, "Blue Denim Jeans", 49.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "DiscountPercentage", "InStock", "IsMain", "IsNew", "Name", "Price" },
                values: new object[] { "A cozy red hoodie perfect for chilly days.", 5, true, false, true, "Red Hoodie", 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DiscountPercentage", "InStock", "IsMain", "IsNew", "Name", "Price" },
                values: new object[] { "Stylish black leather jacket for a bold look.", 15, false, true, false, "Black Leather Jacket", 99.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "Name", "Price" },
                values: new object[] { 2, "Casual green chinos made from breathable fabric.", 0, "Green Chinos", 44.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "Name", "Price" },
                values: new object[] { 5, "Lightweight yellow dress perfect for summer outings.", 20, "Yellow Summer Dress", 59.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "IsNew", "Name", "Price" },
                values: new object[] { 3, "Comfortable gray sweatpants for lounging or workouts.", 0, true, "Gray Sweatpants", 29.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "IsMain", "IsNew", "Name", "Price" },
                values: new object[] { 4, "Elegant navy blue blazer suitable for formal occasions.", 10, true, false, "Navy Blue Blazer", 79.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "DiscountPercentage", "ImageUrl", "InStock", "IsMain", "IsNew", "Name", "Price" },
                values: new object[,]
                {
                    { 9, 5, "Charming pink skirt with floral patterns.", 0, "product-9.jpg", true, false, true, "Pink Floral Skirt", 34.99m },
                    { 10, 6, "Durable brown ankle boots for everyday wear.", 15, "product-10.jpg", true, false, false, "Brown Ankle Boots", 89.99m },
                    { 11, 6, "Classic white sneakers that go with any outfit.", 0, "product-11.jpg", true, false, true, "White Sneakers", 59.99m },
                    { 12, 6, "Sleek black formal shoes for special occasions.", 20, "product-12.jpg", false, true, false, "Black Formal Shoes", 99.99m },
                    { 13, 1, "Breathable orange t-shirt designed for sports activities.", 0, "product-13.jpg", true, false, true, "Orange Sports T-Shirt", 24.99m },
                    { 14, 1, "Lightweight shorts perfect for running and workouts.", 5, "product-14.jpg", true, false, true, "Green Running Shorts", 19.99m },
                    { 15, 2, "Durable gloves for weightlifting and fitness training.", 0, "product-15.jpg", true, false, true, "Black Fitness Gloves", 15.50m },
                    { 16, 2, "Non-slip yoga mat for indoor and outdoor exercises.", 10, "product-16.jpg", true, false, true, "Blue Yoga Mat", 35.00m },
                    { 17, 3, "Spacious gym bag with multiple compartments.", 0, "product-17.jpg", true, false, false, "Red Gym Bag", 45.00m },
                    { 18, 3, "Comfortable sneakers suitable for daily wear.", 15, "product-18.jpg", true, false, true, "White Sneakers", 75.00m },
                    { 19, 4, "Soft cotton hoodie with a minimalist design.", 5, "product-19.jpg", true, false, false, "Black Hoodie", 55.00m },
                    { 20, 4, "Comfortable sweatpants for lounging or workouts.", 0, "product-20.jpg", true, false, false, "Gray Sweatpants", 40.00m },
                    { 21, 5, "Supportive sports bra for high-intensity workouts.", 0, "product-21.jpg", true, false, true, "Pink Sports Bra", 30.00m },
                    { 22, 5, "Lightweight shoes designed for running efficiency.", 10, "product-22.jpg", true, false, true, "Navy Running Shoes", 85.00m },
                    { 23, 1, "Adjustable sports cap to keep the sun away.", 0, "product-23.jpg", true, false, true, "Yellow Cap", 12.99m },
                    { 24, 1, "Elastic headband for fitness and running.", 0, "product-24.jpg", true, false, true, "Purple Headband", 7.99m },
                    { 25, 2, "Reusable sports bottle with 1L capacity.", 0, "product-25.jpg", true, false, true, "Orange Water Bottle", 14.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DiscountPercentage", "Price" },
                values: new object[] { "100% cotton, everyday essential t-shirt.", 10, 25m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "DiscountPercentage", "IsMain", "Name", "Price" },
                values: new object[] { "Modern slim fit jeans with stretch fabric.", 0, true, "Slim Fit Jeans", 80m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "DiscountPercentage", "InStock", "IsMain", "IsNew", "Name", "Price" },
                values: new object[] { "Premium leather jacket for all seasons.", 15, false, true, false, "Leather Jacket", 220m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DiscountPercentage", "InStock", "IsMain", "IsNew", "Name", "Price" },
                values: new object[] { "Lightweight floral summer dress.", 20, true, false, true, "Summer Dress", 65m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "Name", "Price" },
                values: new object[] { 5, "Comfortable sneakers for daily use.", 5, "Sport Sneakers", 110m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "Name", "Price" },
                values: new object[] { 1, "Warm hoodie with minimalist design.", 0, "Hoodie", 55m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "IsNew", "Name", "Price" },
                values: new object[] { 2, "Perfect shirt for office and events.", 10, false, "Formal Shirt", 70m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "DiscountPercentage", "IsMain", "IsNew", "Name", "Price" },
                values: new object[] { 3, "Heavy winter coat with insulation.", 25, false, true, "Winter Coat", 180m });
        }
    }
}
