using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalWebService.Migrations
{
    public partial class DataPoint_Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cough",
                table: "DataPoint",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dizziness",
                table: "DataPoint",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Headache",
                table: "DataPoint",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Nausea",
                table: "DataPoint",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rheum",
                table: "DataPoint",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Weakness",
                table: "DataPoint",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cough",
                table: "DataPoint");

            migrationBuilder.DropColumn(
                name: "Dizziness",
                table: "DataPoint");

            migrationBuilder.DropColumn(
                name: "Headache",
                table: "DataPoint");

            migrationBuilder.DropColumn(
                name: "Nausea",
                table: "DataPoint");

            migrationBuilder.DropColumn(
                name: "Rheum",
                table: "DataPoint");

            migrationBuilder.DropColumn(
                name: "Weakness",
                table: "DataPoint");
        }
    }
}
