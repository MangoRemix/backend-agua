using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePartidoPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetalleAlcaldia",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetalleGobernacion",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetalleInstitucionNacional",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Partido",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieneAlcaldia",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneGobernacion",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneInstitucionNacional",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TienePartido",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetalleAlcaldia",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "DetalleGobernacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "DetalleInstitucionNacional",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "Partido",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneAlcaldia",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneGobernacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneInstitucionNacional",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TienePartido",
                table: "Reportes");
        }
    }
}
