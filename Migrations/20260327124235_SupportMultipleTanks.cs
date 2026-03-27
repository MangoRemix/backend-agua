using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class SupportMultipleTanks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTanque",
                table: "ReporteSuministros");

            migrationBuilder.CreateTable(
                name: "Tanques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporteSuministroId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tanques_ReporteSuministros_ReporteSuministroId",
                        column: x => x.ReporteSuministroId,
                        principalTable: "ReporteSuministros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tanques_ReporteSuministroId",
                table: "Tanques",
                column: "ReporteSuministroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tanques");

            migrationBuilder.AddColumn<int>(
                name: "TipoTanque",
                table: "ReporteSuministros",
                type: "integer",
                nullable: true);
        }
    }
}
