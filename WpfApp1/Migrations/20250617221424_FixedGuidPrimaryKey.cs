using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class FixedGuidPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Uid);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Uid", "CreatedDate", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Игровой ноутбук", "Ноутбук", 85000m, 5 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Беспроводная мышь", "Мышь", 2500m, 15 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Механическая клавиатура", "Клавиатура", 7500m, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
