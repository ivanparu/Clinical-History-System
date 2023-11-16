using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historias_C.Migrations
{
    /// <inheritdoc />
    public partial class MatriculaUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personas_Matricula",
                table: "Personas",
                column: "Matricula",
                unique: true,
                filter: "[Matricula] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personas_Matricula",
                table: "Personas");
        }
    }
}
