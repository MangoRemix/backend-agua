using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class ModularReportStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonasAfectadas_Reportes_ReporteId",
                table: "PersonasAfectadas");

            migrationBuilder.DropColumn(
                name: "ApoyoAdicionalLitros",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "CantidadCasosDiarrea",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "Caudal",
                table: "Reportes");

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
                name: "DetalleAlcaldia",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "DetalleGobernacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "DetalleInstitucionNacional",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FamiliasBeneficiadas",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FugaLugar",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FugaTipo",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "HorasSuministro",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "LitrosCisterna",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "LlegaPorTuberia",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "Partido",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "RecibeCisterna",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneAlcaldia",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneConflictos",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneDiarrea",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneDolorAbdominal",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneFugas",
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

            migrationBuilder.DropColumn(
                name: "TieneTanque",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneTrancas",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneVentaIlegal",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TieneVomitos",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TipoCisterna",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "TipoTanque",
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

            migrationBuilder.RenameColumn(
                name: "ReporteId",
                table: "PersonasAfectadas",
                newName: "ReporteSaludId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonasAfectadas_ReporteId",
                table: "PersonasAfectadas",
                newName: "IX_PersonasAfectadas_ReporteSaludId");

            migrationBuilder.CreateTable(
                name: "ReporteIncidencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteId = table.Column<Guid>(type: "uuid", nullable: false),
                    TieneVentaIlegal = table.Column<bool>(type: "boolean", nullable: false),
                    ChoferNombreApellido = table.Column<string>(type: "text", nullable: true),
                    ChoferCedula = table.Column<string>(type: "text", nullable: true),
                    VehiculoMarcaModelo = table.Column<string>(type: "text", nullable: true),
                    VehiculoPlaca = table.Column<string>(type: "text", nullable: true),
                    VehiculoColor = table.Column<string>(type: "text", nullable: true),
                    TieneTrancas = table.Column<bool>(type: "boolean", nullable: false),
                    TrancaPropiciaNombre = table.Column<string>(type: "text", nullable: true),
                    TrancaLugar = table.Column<string>(type: "text", nullable: true),
                    TrancaDuracion = table.Column<string>(type: "text", nullable: true),
                    TieneConflictos = table.Column<bool>(type: "boolean", nullable: false),
                    ConflictosExplicacion = table.Column<string>(type: "text", nullable: true),
                    TieneFugas = table.Column<bool>(type: "boolean", nullable: false),
                    FugaLugar = table.Column<string>(type: "text", nullable: true),
                    FugaTipo = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteIncidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporteIncidencias_Reportes_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteParticipaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteId = table.Column<Guid>(type: "uuid", nullable: false),
                    TienePartido = table.Column<bool>(type: "boolean", nullable: false),
                    Partido = table.Column<int>(type: "integer", nullable: true),
                    TieneAlcaldia = table.Column<bool>(type: "boolean", nullable: false),
                    DetalleAlcaldia = table.Column<string>(type: "text", nullable: true),
                    TieneGobernacion = table.Column<bool>(type: "boolean", nullable: false),
                    DetalleGobernacion = table.Column<string>(type: "text", nullable: true),
                    TieneInstitucionNacional = table.Column<bool>(type: "boolean", nullable: false),
                    DetalleInstitucionNacional = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteParticipaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporteParticipaciones_Reportes_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteSalud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteId = table.Column<Guid>(type: "uuid", nullable: false),
                    TieneDiarrea = table.Column<bool>(type: "boolean", nullable: false),
                    CantidadCasosDiarrea = table.Column<int>(type: "integer", nullable: false),
                    TieneVomitos = table.Column<bool>(type: "boolean", nullable: false),
                    TieneDolorAbdominal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteSalud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporteSalud_Reportes_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReporteSuministros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteId = table.Column<Guid>(type: "uuid", nullable: false),
                    LlegaPorTuberia = table.Column<bool>(type: "boolean", nullable: false),
                    HorasSuministro = table.Column<int>(type: "integer", nullable: true),
                    Caudal = table.Column<int>(type: "integer", nullable: true),
                    RecibeCisterna = table.Column<bool>(type: "boolean", nullable: false),
                    LitrosCisterna = table.Column<int>(type: "integer", nullable: true),
                    TipoCisterna = table.Column<int>(type: "integer", nullable: true),
                    TieneTanque = table.Column<bool>(type: "boolean", nullable: false),
                    TipoTanque = table.Column<int>(type: "integer", nullable: true),
                    FamiliasBeneficiadas = table.Column<int>(type: "integer", nullable: false),
                    ApoyoAdicionalLitros = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporteSuministros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReporteSuministros_Reportes_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReporteIncidencias_ReporteId",
                table: "ReporteIncidencias",
                column: "ReporteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReporteParticipaciones_ReporteId",
                table: "ReporteParticipaciones",
                column: "ReporteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReporteSalud_ReporteId",
                table: "ReporteSalud",
                column: "ReporteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReporteSuministros_ReporteId",
                table: "ReporteSuministros",
                column: "ReporteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasAfectadas_ReporteSalud_ReporteSaludId",
                table: "PersonasAfectadas",
                column: "ReporteSaludId",
                principalTable: "ReporteSalud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonasAfectadas_ReporteSalud_ReporteSaludId",
                table: "PersonasAfectadas");

            migrationBuilder.DropTable(
                name: "ReporteIncidencias");

            migrationBuilder.DropTable(
                name: "ReporteParticipaciones");

            migrationBuilder.DropTable(
                name: "ReporteSalud");

            migrationBuilder.DropTable(
                name: "ReporteSuministros");

            migrationBuilder.RenameColumn(
                name: "ReporteSaludId",
                table: "PersonasAfectadas",
                newName: "ReporteId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonasAfectadas_ReporteSaludId",
                table: "PersonasAfectadas",
                newName: "IX_PersonasAfectadas_ReporteId");

            migrationBuilder.AddColumn<int>(
                name: "ApoyoAdicionalLitros",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadCasosDiarrea",
                table: "Reportes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Caudal",
                table: "Reportes",
                type: "integer",
                nullable: true);

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
                name: "FamiliasBeneficiadas",
                table: "Reportes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "HorasSuministro",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LitrosCisterna",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LlegaPorTuberia",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Partido",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RecibeCisterna",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneAlcaldia",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneConflictos",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
                name: "TieneFugas",
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

            migrationBuilder.AddColumn<bool>(
                name: "TieneTanque",
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

            migrationBuilder.AddColumn<bool>(
                name: "TieneVomitos",
                table: "Reportes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TipoCisterna",
                table: "Reportes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoTanque",
                table: "Reportes",
                type: "integer",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PersonasAfectadas_Reportes_ReporteId",
                table: "PersonasAfectadas",
                column: "ReporteId",
                principalTable: "Reportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
