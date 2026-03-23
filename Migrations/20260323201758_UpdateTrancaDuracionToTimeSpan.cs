using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrancaDuracionToTimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Limpiar la columna antes de convertirla para evitar errores de cast en PostgreSQL
            migrationBuilder.Sql("UPDATE \"ReporteIncidencias\" SET \"TrancaDuracion\" = NULL;");
            migrationBuilder.Sql("ALTER TABLE \"ReporteIncidencias\" ALTER COLUMN \"TrancaDuracion\" TYPE interval USING \"TrancaDuracion\"::interval;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrancaDuracion",
                table: "ReporteIncidencias",
                type: "text",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);
        }
    }
}
