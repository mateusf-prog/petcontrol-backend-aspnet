using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetControlSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Pets",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Pets",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
