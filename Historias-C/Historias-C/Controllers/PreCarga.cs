using Historias_C.Data;
using Historias_C.Helpers;
using Historias_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Historias_C.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasCContext _context;
        private readonly SignInManager<Persona> _signInManager;


        private List<string> roles = new List<string>() { Configs.MedicoRolName, Configs.EmpleadoRolName, Configs.PacienteRolName };
        public PreCarga(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistoriasClinicasCContext context, SignInManager<Persona> signInManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
            this._signInManager = signInManager;
        }
        public async Task<IActionResult> SeedAsync()
        {
            CrearRoles().Wait();

            if (!_context.Empleados.Any() && !_context.Empleados.Any())
            {
                await creacionPrimaria();
            }

            return RedirectToAction("Index", "Home", new { mensaje = "se ejecutó la pre-carga" });
        }

        private async Task creacionPrimaria()
        {
            //Crea medico
            Medico medico1 = new Medico()
            {
                Especialidad = Especialidad.ALERGIA_E_INMUNOLOGIA,
                Matricula = "S1212NM",
                Email = "medico1@ort.edu.ar",
                UserName = "medico1@ort.edu.ar",
                Nombre = "Medico",
                Apellido = "Alergista",
                DNI = 12457896,
                Telefono = 1145568978,
            };
            var resultadoNewMedico = await _userManager.CreateAsync(medico1, Configs.PasswordDef);
            if (resultadoNewMedico.Succeeded)
            {
                await _userManager.AddToRoleAsync(medico1, Configs.MedicoRolName);
                await _context.SaveChangesAsync();
            }

            //Crea medico2
            Medico medico2 = new Medico()
            {
                Especialidad = Especialidad.CUIDADOS_INTENSIVOS,
                Matricula = "12122NM",
                Email = "medico2@ort.edu.ar",
                UserName = "medico2@ort.edu.ar",
                Nombre = "Médico",
                Apellido = "Cuidados",
                DNI = 48982211,
                Telefono = 1163561177,
            };
            var resultadoNewMedico2 = await _userManager.CreateAsync(medico2, Configs.PasswordDef);
            if (resultadoNewMedico2.Succeeded)
            {
                await _userManager.AddToRoleAsync(medico2, Configs.MedicoRolName);
                await _context.SaveChangesAsync();
            }

            //Crea paciente1

            Paciente paciente1 = new Paciente()
            {
                ObraSocial = ObraSocial.OSDE,
                UserName = "paciente1@ort.edu.ar",
                Email = "paciente1@ort.edu.ar",
                Nombre = "Paciente",
                Apellido = "Osde",
                DNI = 456568989,
                Telefono = 1132326565,
            };
            var resultadoNewPaciente = await _userManager.CreateAsync(paciente1, Configs.PasswordDef);
            if (resultadoNewPaciente.Succeeded)
            {
                await _userManager.AddToRoleAsync(paciente1, Configs.PacienteRolName);

                HistoriaClinica hc = new HistoriaClinica()
                {
                    PacienteId = paciente1.Id,
                };
                _context.HistoriaClinicas.Add(hc);
                await _context.SaveChangesAsync();
            }

            //Creo paciente 2

            Paciente paciente2 = new Paciente()
            {
                ObraSocial = ObraSocial.Galeno,
                UserName = "paciente2@ort.edu.ar",
                Email = "paciente2@ort.edu.ar",
                Nombre = "Paciente",
                Apellido = "Galeno",
                DNI = 34765444,
                Telefono = 1132446565,
            };
            var resultadoNewPaciente2 = await _userManager.CreateAsync(paciente2, Configs.PasswordDef);
            if (resultadoNewPaciente2.Succeeded)
            {
                await _userManager.AddToRoleAsync(paciente2, Configs.PacienteRolName);

                HistoriaClinica hc2 = new HistoriaClinica()
                {
                    PacienteId = paciente2.Id,
                };
                _context.HistoriaClinicas.Add(hc2);
                await _context.SaveChangesAsync();
            }

            //Creo empleado 1

            Empleado empleado1 = new Empleado()
            {
                UserName = "empleado1@ort.edu.ar",
                Email = "empleado1@ort.edu.ar",
                Nombre = "Empleado1",
                Apellido = "Admiistrativo",
                DNI = 54546868,
                Telefono = 1145474847,
            };
            var resultadoNewEmpleado = await _userManager.CreateAsync(empleado1, Configs.PasswordDef);
            if (resultadoNewEmpleado.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado1, Configs.EmpleadoRolName);
                await _context.SaveChangesAsync();
            }

            //Creo empleado 2

            Empleado empleado2 = new Empleado()
            {
                UserName = "empleado2@ort.edu.ar",
                Email = "empleado2@ort.edu.ar",
                Nombre = "Empleado2",
                Apellido = "Secretaria",
                DNI = 29344727,
                Telefono = 132550299,
            };
            var resultadoNewEmpleado2 = await _userManager.CreateAsync(empleado2, Configs.PasswordDef);
            if (resultadoNewEmpleado2.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado2, Configs.EmpleadoRolName);
                await _context.SaveChangesAsync();
            }

            //Agrego episodios

            await addEpisodios(empleado1.Id, paciente1.HistoriaClinica.Id, paciente2.HistoriaClinica.Id, medico1.Id);

        }

       

        private async Task addEpisodios(int empleado1Id, int paciente1hcId, int paciente2hcId, int medicoId)
        {
            Episodio episodio = new Episodio()
            {
                Motivo = "Suba de la presión arterial.",
                Descripcion = "Parámetros del corazon alterados.",
                HistoriaClinicaId = paciente1hcId,
                EmpleadoId= empleado1Id
            };

            Episodio episodio2 = new Episodio()
            {
                Motivo = "Poca coagulación de aorta.",
                Descripcion = "Valores de aorta bloqueantes.",
                HistoriaClinicaId = paciente1hcId,
                EmpleadoId = empleado1Id,
                FechaYHoraInicio = DateTime.Now.AddDays(-3)
            };

            Episodio episodio3 = new Episodio()
            {
                Motivo = "Arrtimia grado 2.",
                Descripcion = "Latidos con latencia -23.",
                HistoriaClinicaId = paciente1hcId,
                EmpleadoId = empleado1Id,
                FechaYHoraInicio = DateTime.Now.AddDays(-2)
            };

            Episodio episodio4 = new Episodio()
            {
                Motivo = "Neurotorax traumático tipo 2.",
                Descripcion = "Poca compresion pulmonar.",
                HistoriaClinicaId = paciente2hcId,
                EmpleadoId = empleado1Id,
                FechaYHoraInicio = DateTime.Now.AddDays(-2),
                EstadoAbierto = true

            };

            Episodio episodioCerrado = new Episodio()
            {
                Motivo = "Descenso de la presión arterial.",
                Descripcion = "Poca presion sanguinea.",
                HistoriaClinicaId = paciente2hcId,
                FechaYHoraCierre = DateTime.Now,
                EstadoAbierto = false,
                EmpleadoId = empleado1Id

            };
            _context.Episodios.Add(episodio);
            _context.Episodios.Add(episodio2);
            _context.Episodios.Add(episodio3);
            _context.Episodios.Add(episodio4);
            _context.Episodios.Add(episodioCerrado);
            await _context.SaveChangesAsync();

            await addEvolucion(medicoId, episodioCerrado.Id, empleado1Id);
            await addEpicrisis(medicoId, episodioCerrado.Id);
        }
            private async Task addEvolucion(int medicoId, int episodioCerradoId, int empleado1Id)
        {
            Evolucion evolucion = new Evolucion()
            {
                MedicoId = medicoId,
                EpisodioId = episodioCerradoId,
                DescripcionAtencion = "El paciente evoluciona favorablemente.",
                FechaYHoraCierre= DateTime.Now,
                EstadoAbierto = false

            };

            Evolucion evolucion2 = new Evolucion()
            {
                MedicoId = medicoId,
                EpisodioId = episodioCerradoId,
                DescripcionAtencion = "Continúa en observacion de glóbulos blancos.",
                FechaYHoraCierre = DateTime.Now.AddDays(-2).AddHours(-3),
                EstadoAbierto = false,
                FechaYHoraInicio = DateTime.Now.AddDays(-5).AddHours(-11),
            };

            Evolucion evolucion3 = new Evolucion()
            {
                MedicoId = medicoId,
                EpisodioId = episodioCerradoId,
                DescripcionAtencion = "Estabilización conseguida.",
                FechaYHoraCierre = DateTime.Now.AddDays(-1).AddHours(-6),
                EstadoAbierto = false,
                FechaYHoraInicio = DateTime.Now.AddDays(-4).AddHours(-11),
            };

            _context.Evoluciones.Add(evolucion);
            _context.Evoluciones.Add(evolucion2);
            _context.Evoluciones.Add(evolucion3);


            await _context.SaveChangesAsync();
            await addNota(evolucion.Id, empleado1Id);
        }

        private async Task addEpicrisis(int medicoId, int episodioCerradoId)
        {
            Epicrisis epicrisis = new Epicrisis()
            {
                MedicoId = medicoId,
                EpisodioId = episodioCerradoId,
                Descripcion = "Arteria femoral obstruida.",
                Recomendacion = "Valcote 35mg cada 12 hs"

            };

            _context.Epicrisis.Add(epicrisis);
            await _context.SaveChangesAsync();
            /*var episodio = _context.Episodios.Find(episodioCerradoId);
            episodio.EpicrisisId = epicrisis.Id;*/
        }

        private async Task addNota(int evolucionId, int empleado1Id)
        {
            Notas nota = new Notas()
            {
                EvolucionId = evolucionId,
                EmpleadoId = empleado1Id,
                Mensaje = "Arteria femoral obstruida.",
                FechaYHora = DateTime.Now.AddDays(-1).AddHours(4)

            };
            Notas nota2 = new Notas()
            {
                EvolucionId = evolucionId,
                EmpleadoId = empleado1Id,
                Mensaje = "Aumentarle Enalapril 20mg cada 12 hs.",
                FechaYHora = DateTime.Now


            };
            Notas nota3 = new Notas()
            {
                EvolucionId = evolucionId,
                EmpleadoId = empleado1Id,
                Mensaje = "Moexipril cada 2 hs. Ver goteo.",
                FechaYHora = DateTime.Now.AddDays(-3).AddHours(2)

            };

            _context.Notas.Add(nota);
            _context.Notas.Add(nota2);
            _context.Notas.Add(nota3);

            await _context.SaveChangesAsync();

        }
        public async Task<IActionResult> RecreateAsync()
        {

            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { mensaje = "se regeneró la base de datos" });
        }
        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                   await _roleManager.CreateAsync(new Rol(rolName));

                }
            }
        }
    }
}
