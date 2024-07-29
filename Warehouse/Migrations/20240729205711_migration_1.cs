using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class migration_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Computer Keyboard" },
                    { 2, "Computer Mouse" },
                    { 3, "Computer Motherboard" },
                    { 4, "Computer Ram" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "FullName", "ISActive", "Password", "UserName" },
                values: new object[] { 1, "admin@gmail.com", "Test Admin", true, "pass1234", "Admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "Logitech MK235 Wireless Keyboard and Mouse Combo for Windows, 2.4 GHz Wireless with Unifying USB-Receiver, Wireless Mouse, 15 FN Keys, 3-Year", "MK235 Wireless Keyboard", 275m, 50 },
                    { 2, 1, "SteelSeries Apex Pro TKL HyperMagnetic Gaming Keyboard - World's Fastest Keyboard - Adjustable Actuation - Esports TKL - OLED Screen - PBT Keycaps", "SteelSeries Gaming Keyboard", 820m, 20 },
                    { 3, 2, "Logitech G305 Lightspeed Wireless Gaming Mouse, HERO Sensor, 12,000 DPI, Lightweight, 6 Programmable Buttons, 250h Battery Life, On-Board Memory,", "Logitech G305 Wireless Mouse", 248m, 25 },
                    { 4, 3, "MSI PRO B650-S WiFi ProSeries Motherboard (Supports AMD Ryzen 7000 Series Processors, AM5, DDR5, PCIe 4.0, M.2 Slots, SATA 6Gb/s, USB 3.2 Gen 2,", "MSI PRO B650-S WiFi ProSeries Motherboard", 700m, 30 },
                    { 5, 3, "ASRock B660M Pro RS Intel B660 Series CPU (LGA1700) Compatible B660M MicroATX Motherboard", "ASRock B660M Pro RS Intel", 248m, 45 },
                    { 6, 3, "Intel Core i7-14700K New Gaming Desktop Processor 20 cores (8 P-cores + 12 E-cores) with Integrated Graphics - Unlocked", "Intel Core i7-14700K Motherboard", 700m, 50 },
                    { 7, 4, "HPE P00924-B21 memory module 32 GB DDR4 2933 MHz", "HPE DDR4 RAM 32GB", 900m, 20 },
                    { 8, 4, "CORSAIR VENGEANCE RGB DDR5 RAM 32GB (2x16GB) 6400MHz CL36 Intel XMP iCUE Compatible Computer Memory - Black (CMH32GX5M2B6400C36)", "CORSAIR DDR5 RAM 32GB", 75m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
