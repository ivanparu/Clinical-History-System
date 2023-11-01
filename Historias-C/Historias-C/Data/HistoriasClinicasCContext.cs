using Historias_C.Models;
using Microsoft.EntityFrameworkCore;

namespace Historias_C.Data
{
    public class HistoriasClinicasCContext : DbContext
    {
        public HistoriasClinicasCContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Episodio>()
                .HasOne(e => e.HistoriaClinica)
                .WithMany(h => h.Episodios)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<HistoriaClinica> HistoriaClinicas { get; set; }
        public DbSet<Evolucion> Evoluciones { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Epicrisis> Epicrisis { get; set; }
        

    }
}
