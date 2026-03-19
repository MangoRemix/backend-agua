using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddLideresOptionalToComunaYComunidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LiderCedula",
                table: "Comunidades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderNombre",
                table: "Comunidades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderCedula",
                table: "Comunas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderNombre",
                table: "Comunas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiderCedula",
                table: "Comunidades");

            migrationBuilder.DropColumn(
                name: "LiderNombre",
                table: "Comunidades");

            migrationBuilder.DropColumn(
                name: "LiderCedula",
                table: "Comunas");

            migrationBuilder.DropColumn(
                name: "LiderNombre",
                table: "Comunas");
        }
    }
}
