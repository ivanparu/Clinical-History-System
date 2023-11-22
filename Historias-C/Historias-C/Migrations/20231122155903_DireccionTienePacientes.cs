using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historias_C.Migrations
{
    /// <inheritdoc />
    public partial class DireccionTienePacientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Personas_PersonaId",
                table: "Direcciones");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Direcciones",
                newName: "PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_PersonaId",
                table: "Direcciones",
                newName: "IX_Direcciones_PacienteId");

            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_DireccionId",
                table: "Personas",
                column: "DireccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Personas_PacienteId",
                table: "Direcciones",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Direcciones_DireccionId",
                table: "Personas",
                column: "DireccionId",
                principalTable: "Direcciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Personas_PacienteId",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Direcciones_DireccionId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_DireccionId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Direcciones",
                newName: "PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_PacienteId",
                table: "Direcciones",
                newName: "IX_Direcciones_PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Personas_PersonaId",
                table: "Direcciones",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
