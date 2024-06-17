using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSim.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class honk_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Horn",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horn",
                table: "Cars");
        }
    }
}
