#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants.Infrastructure.Database.Migrations.ApplicationMigrations;

/// <inheritdoc />
public partial class AjoutDuOwnerNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Addresses",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                City = table.Column<string>("nvarchar(max)", nullable: false),
                Street = table.Column<string>("nvarchar(max)", nullable: false),
                PostalCode = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Addresses", x => x.Id); });

        migrationBuilder.CreateTable(
            "Restaurants",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: false),
                Category = table.Column<string>("nvarchar(max)", nullable: false),
                HasDelivery = table.Column<bool>("bit", nullable: false),
                ContactEmail = table.Column<string>("nvarchar(max)", nullable: true),
                ContactNumber = table.Column<string>("nvarchar(max)", nullable: true),
                AddressId = table.Column<int>("int", nullable: false),
                OwnerId = table.Column<int>("int", nullable: false, defaultValue: 1)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Restaurants", x => x.Id);
                table.ForeignKey(
                    "FK_Restaurants_Addresses_AddressId",
                    x => x.AddressId,
                    "Addresses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_Restaurants_Users_OwnerId",
                    x => x.OwnerId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Dishes",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: false),
                Price = table.Column<decimal>("decimal(18,2)", nullable: false),
                RestaurantId = table.Column<int>("int", nullable: false),
                KiloCalories = table.Column<int>("int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Dishes", x => x.Id);
                table.ForeignKey(
                    "FK_Dishes_Restaurants_RestaurantId",
                    x => x.RestaurantId,
                    "Restaurants",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_Dishes_RestaurantId",
            "Dishes",
            "RestaurantId");

        migrationBuilder.CreateIndex(
            "IX_Restaurants_AddressId",
            "Restaurants",
            "AddressId");

        migrationBuilder.CreateIndex(
            "IX_Restaurants_OwnerId",
            "Restaurants",
            "OwnerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Dishes");

        migrationBuilder.DropTable(
            "Restaurants");

        migrationBuilder.DropTable(
            "Addresses");
    }
}