using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historias_C.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Matricula = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Especialidad = table.Column<int>(type: "int", nullable: true),
                    ObraSocial = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Altura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direcciones_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriaClinicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaClinicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoriaClinicas_Personas_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Epicrisis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpisodioId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    FechaYHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Recomendacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epicrisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epicrisis_Personas_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    FechaYHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaYHoraCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoAbierto = table.Column<bool>(type: "bit", nullable: false),
                    EpicrisisId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    HistoriaClinicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodios_Epicrisis_EpicrisisId",
                        column: x => x.EpicrisisId,
                        principalTable: "Epicrisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episodios_HistoriaClinicas_HistoriaClinicaId",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriaClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Episodios_Personas_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evoluciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    FechaYHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaYHoraCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescripcionAtencion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EstadoAbierto = table.Column<bool>(type: "bit", nullable: false),
                    EpisodioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evoluciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evoluciones_Episodios_EpisodioId",
                        column: x => x.EpisodioId,
                        principalTable: "Episodios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evoluciones_Personas_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvolucionId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    FechaYHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Evoluciones_EvolucionId",
                        column: x => x.EvolucionId,
                        principalTable: "Evoluciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notas_Personas_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_PersonaId",
                table: "Direcciones",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Epicrisis_EpisodioId",
                table: "Epicrisis",
                column: "EpisodioId");

            migrationBuilder.CreateIndex(
                name: "IX_Epicrisis_MedicoId",
                table: "Epicrisis",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_EmpleadoId",
                table: "Episodios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_EpicrisisId",
                table: "Episodios",
                column: "EpicrisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_HistoriaClinicaId",
                table: "Episodios",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evoluciones_EpisodioId",
                table: "Evoluciones",
                column: "EpisodioId");

            migrationBuilder.CreateIndex(
                name: "IX_Evoluciones_MedicoId",
                table: "Evoluciones",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaClinicas_PacienteId",
                table: "HistoriaClinicas",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EmpleadoId",
                table: "Notas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EvolucionId",
                table: "Notas",
                column: "EvolucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Epicrisis_Episodios_EpisodioId",
                table: "Epicrisis",
                column: "EpisodioId",
                principalTable: "Episodios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicrisis_Personas_MedicoId",
                table: "Epicrisis");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodios_Personas_EmpleadoId",
                table: "Episodios");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoriaClinicas_Personas_PacienteId",
                table: "HistoriaClinicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Epicrisis_Episodios_EpisodioId",
                table: "Epicrisis");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Evoluciones");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Episodios");

            migrationBuilder.DropTable(
                name: "Epicrisis");

            migrationBuilder.DropTable(
                name: "HistoriaClinicas");
        }
    }
}
