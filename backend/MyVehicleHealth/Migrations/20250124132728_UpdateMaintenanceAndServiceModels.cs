using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVehicleHealth.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaintenanceAndServiceModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Services",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
