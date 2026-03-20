using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class TerritorialUserHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComunaId",
                table: "Usuarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParroquiaId",
                table: "Usuarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ComunaId",
                table: "Usuarios",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ParroquiaId",
                table: "Usuarios",
                column: "ParroquiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Comunas_ComunaId",
                table: "Usuarios",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Parroquias_ParroquiaId",
                table: "Usuarios",
                column: "ParroquiaId",
                principalTable: "Parroquias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Comunas_ComunaId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Parroquias_ParroquiaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ComunaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ParroquiaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ComunaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ParroquiaId",
                table: "Usuarios");
        }
    }
}
