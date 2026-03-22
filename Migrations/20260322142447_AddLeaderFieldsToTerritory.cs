using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaderFieldsToTerritory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LiderCedula",
                table: "Parroquias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderNombre",
                table: "Parroquias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderTlf",
                table: "Parroquias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderTlf",
                table: "Comunidades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiderTlf",
                table: "Comunas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiderCedula",
                table: "Parroquias");

            migrationBuilder.DropColumn(
                name: "LiderNombre",
                table: "Parroquias");

            migrationBuilder.DropColumn(
                name: "LiderTlf",
                table: "Parroquias");

            migrationBuilder.DropColumn(
                name: "LiderTlf",
                table: "Comunidades");

            migrationBuilder.DropColumn(
                name: "LiderTlf",
                table: "Comunas");
        }
    }
}
