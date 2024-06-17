using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSim.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class carmodel_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Cars");
        }
    }
}
