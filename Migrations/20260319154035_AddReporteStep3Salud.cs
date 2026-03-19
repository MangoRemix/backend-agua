using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddReporteStep3Salud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadCasosDiarrea",
                table: "Reportes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TieneDiarrea",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneDolorAbdominal",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneVomitos",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PersonasAfectadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Cedula = table.Column<string>(type: "text", nullable: false),
                    Condicion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasAfectadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonasAfectadas_Reportes_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonasAfectadas_ReporteId",
                table: "PersonasAfectadas",
                column: "ReporteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonasAfectadas");

            migrationBuilder.DropColumn(
                name: "CantidadCasosDiarrea",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneDiarrea",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneDolorAbdominal",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneVomitos",
                table: "Reportes");
        }
    }
}
