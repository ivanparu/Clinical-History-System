using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historias_C.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodios_Epicrisis_EpicrisisId",
                table: "Episodios");

            migrationBuilder.AlterColumn<int>(
                name: "EpicrisisId",
                table: "Episodios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodios_Epicrisis_EpicrisisId",
                table: "Episodios",
                column: "EpicrisisId",
                principalTable: "Epicrisis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodios_Epicrisis_EpicrisisId",
                table: "Episodios");

            migrationBuilder.AlterColumn<int>(
                name: "EpicrisisId",
                table: "Episodios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodios_Epicrisis_EpicrisisId",
                table: "Episodios",
                column: "EpicrisisId",
                principalTable: "Epicrisis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
