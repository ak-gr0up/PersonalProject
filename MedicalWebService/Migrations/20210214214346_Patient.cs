using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalWebService.Migrations
{
    public partial class Patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Person",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Person",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
