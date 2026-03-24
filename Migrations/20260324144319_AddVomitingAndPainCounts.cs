using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddVomitingAndPainCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadCasosDolorAbdominal",
                table: "ReporteSalud",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantidadCasosVomitos",
                table: "ReporteSalud",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadCasosDolorAbdominal",
                table: "ReporteSalud");

            migrationBuilder.DropColumn(
                name: "CantidadCasosVomitos",
                table: "ReporteSalud");
        }
    }
}
