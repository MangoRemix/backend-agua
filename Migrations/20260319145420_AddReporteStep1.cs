using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddReporteStep1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComunidadId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estatus = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reportes_Comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "Comunidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ComunidadId",
                table: "Reportes",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarioId",
                table: "Reportes",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reportes");
        }
    }
}
