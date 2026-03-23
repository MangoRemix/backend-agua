using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_agua.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHorasSuministroToTimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convertir integer a interval asumiendo que el valor anterior representaba horas
            migrationBuilder.Sql("ALTER TABLE \"ReporteSuministros\" ALTER COLUMN \"HorasSuministro\" TYPE interval USING (CASE WHEN \"HorasSuministro\" IS NOT NULL THEN (\"HorasSuministro\" || ' hours')::interval ELSE NULL END);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HorasSuministro",
                table: "ReporteSuministros",
                type: "integer",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);
        }
    }
}
