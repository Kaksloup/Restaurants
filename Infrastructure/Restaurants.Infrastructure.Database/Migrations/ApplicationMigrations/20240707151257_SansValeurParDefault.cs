#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants.Infrastructure.Database.Migrations.ApplicationMigrations;

/// <inheritdoc />
public partial class SansValeurParDefault : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            "OwnerId",
            "Restaurants",
            "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true,
            oldDefaultValue: 1);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            "OwnerId",
            "Restaurants",
            "int",
            nullable: true,
            defaultValue: 1,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);
    }
}