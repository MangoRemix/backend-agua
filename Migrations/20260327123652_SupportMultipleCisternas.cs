using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class SupportMultipleCisternas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LitrosCisterna",
                table: "ReporteSuministros");

            migrationBuilder.DropColumn(
                name: "TipoCisterna",
                table: "ReporteSuministros");

            migrationBuilder.CreateTable(
                name: "Cisternas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteSuministroId = table.Column<Guid>(type: "uuid", nullable: false),
                    Litros = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cisternas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cisternas_ReporteSuministros_ReporteSuministroId",
                        column: x => x.ReporteSuministroId,
                        principalTable: "ReporteSuministros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cisternas_ReporteSuministroId",
                table: "Cisternas",
                column: "ReporteSuministroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cisternas");

            migrationBuilder.AddColumn<int>(
                name: "LitrosCisterna",
                table: "ReporteSuministros",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoCisterna",
                table: "ReporteSuministros",
                type: "integer",
                nullable: true);
        }
    }
}
