﻿// <auto-generated />
using System;
using Historias_C.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Historias_C.Migrations
{
    [DbContext(typeof(HistoriasClinicasCContext))]
    [Migration("20231122155903_DireccionTienePacientes")]
    partial class DireccionTienePacientes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Historias_C.Models.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Altura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Barrio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Calle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId")
                        .IsUnique();

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("Historias_C.Models.Epicrisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("EpisodioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<string>("Recomendacion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("EpisodioId");

                    b.HasIndex("MedicoId");

                    b.ToTable("Epicrisis");
                });

            modelBuilder.Entity("Historias_C.Models.Episodio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int?>("EpicrisisId")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoAbierto")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaYHoraAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaYHoraCierre")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("HistoriaClinicaId")
                        .HasColumnType("int");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("EpicrisisId");

                    b.HasIndex("HistoriaClinicaId");

                    b.ToTable("Episodios");
                });

            modelBuilder.Entity("Historias_C.Models.Evolucion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescripcionAtencion")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("EpisodioId")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoAbierto")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaYHoraAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaYHoraCierre")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EpisodioId");

                    b.HasIndex("MedicoId");

                    b.ToTable("Evoluciones");
                });

            modelBuilder.Entity("Historias_C.Models.HistoriaClinica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId")
                        .IsUnique();

                    b.ToTable("HistoriaClinicas");
                });

            modelBuilder.Entity("Historias_C.Models.Notas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("EvolucionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("EvolucionId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole<int>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Personas", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser<int>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PersonasRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Historias_C.Models.Rol", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole<int>");

                    b.HasDiscriminator().HasValue("Rol");
                });

            modelBuilder.Entity("Historias_C.Models.Persona", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser<int>");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Persona");
                });

            modelBuilder.Entity("Historias_C.Models.Empleado", b =>
                {
                    b.HasBaseType("Historias_C.Models.Persona");

                    b.Property<int?>("DireccionId")
                        .HasColumnType("int");

                    b.Property<string>("Legajo")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasIndex("DireccionId");

                    b.HasDiscriminator().HasValue("Empleado");
                });

            modelBuilder.Entity("Historias_C.Models.Paciente", b =>
                {
                    b.HasBaseType("Historias_C.Models.Persona");

                    b.Property<int>("ObraSocial")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Paciente");
                });

            modelBuilder.Entity("Historias_C.Models.Medico", b =>
                {
                    b.HasBaseType("Historias_C.Models.Empleado");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasIndex("Matricula")
                        .IsUnique()
                        .HasFilter("[Matricula] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Medico");
                });

            modelBuilder.Entity("Historias_C.Models.Direccion", b =>
                {
                    b.HasOne("Historias_C.Models.Paciente", "Paciente")
                        .WithOne("Direccion")
                        .HasForeignKey("Historias_C.Models.Direccion", "PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Historias_C.Models.Epicrisis", b =>
                {
                    b.HasOne("Historias_C.Models.Episodio", "Episodio")
                        .WithMany()
                        .HasForeignKey("EpisodioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Historias_C.Models.Medico", "Medico")
                        .WithMany("Epicrisis")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Episodio");

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Historias_C.Models.Episodio", b =>
                {
                    b.HasOne("Historias_C.Models.Empleado", "Empleado")
                        .WithMany("Episodios")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Historias_C.Models.Epicrisis", "Epicrisis")
                        .WithMany()
                        .HasForeignKey("EpicrisisId");

                    b.HasOne("Historias_C.Models.HistoriaClinica", "HistoriaClinica")
                        .WithMany("Episodios")
                        .HasForeignKey("HistoriaClinicaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Epicrisis");

                    b.Navigation("HistoriaClinica");
                });

            modelBuilder.Entity("Historias_C.Models.Evolucion", b =>
                {
                    b.HasOne("Historias_C.Models.Episodio", "Episodio")
                        .WithMany("Evoluciones")
                        .HasForeignKey("EpisodioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Historias_C.Models.Medico", "Medico")
                        .WithMany("Evoluciones")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Episodio");

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Historias_C.Models.HistoriaClinica", b =>
                {
                    b.HasOne("Historias_C.Models.Paciente", "Paciente")
                        .WithOne("HistoriaClinica")
                        .HasForeignKey("Historias_C.Models.HistoriaClinica", "PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Historias_C.Models.Notas", b =>
                {
                    b.HasOne("Historias_C.Models.Empleado", "Empleado")
                        .WithMany("Notas")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Historias_C.Models.Evolucion", "Evolucion")
                        .WithMany("Notas")
                        .HasForeignKey("EvolucionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Evolucion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Historias_C.Models.Empleado", b =>
                {
                    b.HasOne("Historias_C.Models.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("DireccionId");

                    b.Navigation("Direccion");
                });

            modelBuilder.Entity("Historias_C.Models.Episodio", b =>
                {
                    b.Navigation("Evoluciones");
                });

            modelBuilder.Entity("Historias_C.Models.Evolucion", b =>
                {
                    b.Navigation("Notas");
                });

            modelBuilder.Entity("Historias_C.Models.HistoriaClinica", b =>
                {
                    b.Navigation("Episodios");
                });

            modelBuilder.Entity("Historias_C.Models.Empleado", b =>
                {
                    b.Navigation("Episodios");

                    b.Navigation("Notas");
                });

            modelBuilder.Entity("Historias_C.Models.Paciente", b =>
                {
                    b.Navigation("Direccion");

                    b.Navigation("HistoriaClinica");
                });

            modelBuilder.Entity("Historias_C.Models.Medico", b =>
                {
                    b.Navigation("Epicrisis");

                    b.Navigation("Evoluciones");
                });
#pragma warning restore 612, 618
        }
    }
}
