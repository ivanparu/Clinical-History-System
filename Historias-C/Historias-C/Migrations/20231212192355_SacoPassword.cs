using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historias_C.Migrations
{
    /// <inheritdoc />
    public partial class SacoPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Personas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Personas",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
