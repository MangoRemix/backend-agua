using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class SupportMultiIncidencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConflictosExplicacion",
                table: "ReporteIncidencias");

            migrationBuilder.DropColumn(
                name: "FugaLugar",
                table: "ReporteIncidencias");

            migrationBuilder.DropColumn(
                name: "FugaTipo",
                table: "ReporteIncidencias");

            migrationBuilder.DropColumn(
                name: "TrancaDuracion",
                table: "ReporteIncidencias");

            migrationBuilder.DropColumn(
                name: "TrancaLugar",
                table: "ReporteIncidencias");

            migrationBuilder.DropColumn(
                name: "TrancaPropiciaNombre",
                table: "ReporteIncidencias");

            migrationBuilder.CreateTable(
                name: "Conflictos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteIncidenciaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Explicacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conflictos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conflictos_ReporteIncidencias_ReporteIncidenciaId",
                        column: x => x.ReporteIncidenciaId,
                        principalTable: "ReporteIncidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fugas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteIncidenciaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Lugar = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fugas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fugas_ReporteIncidencias_ReporteIncidenciaId",
                        column: x => x.ReporteIncidenciaId,
                        principalTable: "ReporteIncidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trancas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteIncidenciaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropiciaNombre = table.Column<string>(type: "text", nullable: true),
                    Lugar = table.Column<string>(type: "text", nullable: true),
                    Duracion = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trancas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trancas_ReporteIncidencias_ReporteIncidenciaId",
                        column: x => x.ReporteIncidenciaId,
                        principalTable: "ReporteIncidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conflictos_ReporteIncidenciaId",
                table: "Conflictos",
                column: "ReporteIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fugas_ReporteIncidenciaId",
                table: "Fugas",
                column: "ReporteIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trancas_ReporteIncidenciaId",
                table: "Trancas",
                column: "ReporteIncidenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conflictos");

            migrationBuilder.DropTable(
                name: "Fugas");

            migrationBuilder.DropTable(
                name: "Trancas");

            migrationBuilder.AddColumn<string>(
                name: "ConflictosExplicacion",
                table: "ReporteIncidencias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FugaLugar",
                table: "ReporteIncidencias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FugaTipo",
                table: "ReporteIncidencias",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TrancaDuracion",
                table: "ReporteIncidencias",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrancaLugar",
                table: "ReporteIncidencias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrancaPropiciaNombre",
                table: "ReporteIncidencias",
                type: "text",
                nullable: true);
        }
    }
}
