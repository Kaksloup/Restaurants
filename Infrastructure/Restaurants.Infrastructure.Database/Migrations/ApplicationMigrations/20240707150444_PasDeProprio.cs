#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants.Infrastructure.Database.Migrations.ApplicationMigrations;

/// <inheritdoc />
public partial class PasDeProprio : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Restaurants_Users_OwnerId",
            "Restaurants");

        migrationBuilder.AlterColumn<int>(
            "OwnerId",
            "Restaurants",
            "int",
            nullable: true,
            defaultValue: 1,
            oldClrType: typeof(int),
            oldType: "int",
            oldDefaultValue: 1);

        migrationBuilder.AddForeignKey(
            "FK_Restaurants_Users_OwnerId",
            "Restaurants",
            "OwnerId",
            "Users",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Restaurants_Users_OwnerId",
            "Restaurants");

        migrationBuilder.AlterColumn<int>(
            "OwnerId",
            "Restaurants",
            "int",
            nullable: false,
            defaultValue: 1,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true,
            oldDefaultValue: 1);

        migrationBuilder.AddForeignKey(
            "FK_Restaurants_Users_OwnerId",
            "Restaurants",
            "OwnerId",
            "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}