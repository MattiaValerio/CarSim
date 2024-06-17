using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSim.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Plate = table.Column<string>(type: "text", nullable: false),
                    Engine = table.Column<int>(type: "integer", nullable: false),
                    Body = table.Column<int>(type: "integer", nullable: false),
                    SteeringWheel = table.Column<int>(type: "integer", nullable: false),
                    BrakePedal = table.Column<bool>(type: "boolean", nullable: false),
                    Tank = table.Column<int>(type: "integer", nullable: false),
                    FuelType = table.Column<int>(type: "integer", nullable: false),
                    Accelerator = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Plate);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
