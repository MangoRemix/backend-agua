using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class SupportMultipleSymptoms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condicion",
                table: "PersonasAfectadas");

            migrationBuilder.AddColumn<int[]>(
                name: "Condiciones",
                table: "PersonasAfectadas",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condiciones",
                table: "PersonasAfectadas");

            migrationBuilder.AddColumn<int>(
                name: "Condicion",
                table: "PersonasAfectadas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
