using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddComunidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comunidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    ComunaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comunidades_Comunas_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comunidades_ComunaId",
                table: "Comunidades",
                column: "ComunaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comunidades");
        }
    }
}
