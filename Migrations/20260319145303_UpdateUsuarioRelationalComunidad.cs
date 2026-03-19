using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsuarioRelationalComunidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComunidadId",
                table: "Usuarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ComunidadId",
                table: "Usuarios",
                column: "ComunidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Comunidades_ComunidadId",
                table: "Usuarios",
                column: "ComunidadId",
                principalTable: "Comunidades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Comunidades_ComunidadId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ComunidadId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ComunidadId",
                table: "Usuarios");
        }
    }
}
