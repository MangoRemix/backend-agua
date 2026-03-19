using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddReporteStep2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChoferCedula",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChoferNombreApellido",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConflictosExplicacion",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FugaLugar",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FugaTipo",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieneConflictos",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneFugas",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneTrancas",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneVentaIlegal",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TrancaDuracion",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrancaLugar",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrancaPropiciaNombre",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehiculoColor",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehiculoMarcaModelo",
                table: "Reportes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehiculoPlaca",
                table: "Reportes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChoferCedula",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "ChoferNombreApellido",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "ConflictosExplicacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FugaLugar",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FugaTipo",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneConflictos",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneFugas",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneTrancas",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneVentaIlegal",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TrancaDuracion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TrancaLugar",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TrancaPropiciaNombre",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "VehiculoColor",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "VehiculoMarcaModelo",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "VehiculoPlaca",
                table: "Reportes");
        }
    }
}
