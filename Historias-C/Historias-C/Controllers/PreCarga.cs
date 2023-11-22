using Historias_C.Data;
using Historias_C.Helpers;
using Historias_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Historias_C.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasCContext _context;


        private List<string> roles = new List<string>() { Configs.MedicoRolName, Configs.EmpleadoRolName, Configs.PacienteRolName };
        public PreCarga(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistoriasClinicasCContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }
        public async Task<IActionResult> SeedAsync()
        {
            CrearRoles().Wait();

            if (!_context.Empleados.Any())
            {
                await AddEmpleadosAsync();
            }

            if (!_context.Pacientes.Any())
            {
                 await AddPacientesAsync();
                await AddOtroPacienteAsync();

            }

            if (!_context.Medicos.Any())
            {
                await AddMedicosAsync();
            }
            if (!_context.Empleados.Any())
            {
                await AddOtherEmpleadosAsync();
            }
            if (!_context.Evoluciones.Any())
            {
                await addEvolucion();
            }
            if (!_context.Episodios.Any())
            {
                await addEpisodion();
            }

            if (!_context.Epicrisis.Any())
            {
                await addEpicrisis();
            }

            if (!_context.Notas.Any())
            {
                await addNota();
            }






            return RedirectToAction("Index", "Home", new { mensaje = "se ejecutó la pre-carga" });
        }

        private async Task AddMedicosAsync()
        {
            Medico medico1 = new Medico()
            {
                Id= 40,
                Especialidad = Especialidad.ALERGIA_E_INMUNOLOGIA,
                Matricula = "S1212NM",
                Email = "joacom@ort.edu.ar",
                UserName = "joacom@ort.edu.ar",
                Password = Configs.PasswordDef,
                Nombre = "Joaco",
                Apellido = "Messi",
                DNI = 12457896,
                Telefono = 1145568978,
            };
            var resultadoNewMedico = await _userManager.CreateAsync(medico1, Configs.PasswordDef);
            if (resultadoNewMedico.Succeeded)
            {
                await _userManager.AddToRoleAsync(medico1, Configs.MedicoRolName);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddPacientesAsync()
        {
            Paciente paciente1 = new Paciente()
            {
                Id=41,
                ObraSocial = ObraSocial.OSDE,
                UserName = "sergiol@ort.edu.ar",
                Password = Configs.PasswordDef,
                Email = "sergiol@ort.edu.ar",
                Nombre = "Sergio",
                Apellido = "Leon",
                DNI = 456568989,
                Telefono = 1132326565,
            };
            var resultadoNewPaciente = await _userManager.CreateAsync(paciente1, Configs.PasswordDef);
            if (resultadoNewPaciente.Succeeded)
            {
                await _userManager.AddToRoleAsync(paciente1, Configs.PacienteRolName);
                
                HistoriaClinica hc = new HistoriaClinica()
                {
                    Id = 1212,
                    PacienteId = paciente1.Id,
                };
                _context.Add(hc);
                await _context.SaveChangesAsync();
            }
        }


        private async Task AddOtroPacienteAsync()
        {
            Paciente paciente1 = new Paciente()
            {
                Id = 3,
                ObraSocial = ObraSocial.OSDE,
                UserName = "martinrod@ort.edu.ar",
                Password = Configs.PasswordDef,
                Email = "martinrod@ort.edu.ar",
                Nombre = "Martín",
                Apellido = "Rodriguez",
                DNI = 34765444,
                Telefono = 1132446565,
            };
            var resultadoNewPaciente = await _userManager.CreateAsync(paciente1, Configs.PasswordDef);
            if (resultadoNewPaciente.Succeeded)
            {
                await _userManager.AddToRoleAsync(paciente1, Configs.PacienteRolName);

                HistoriaClinica hc = new HistoriaClinica()
                {
                    Id = 1213,
                    PacienteId = paciente1.Id,
                };
                _context.Add(hc);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddEmpleadosAsync()
        {
            Empleado empleado1 = new Empleado()
            {
                Id= 1,
                UserName = "mariom@ort.edu.ar",
                Password = Configs.PasswordDef,
                Email = "mariom@ort.edu.ar",
                Nombre = "Mario",
                Apellido = "Martinez",
                DNI = 54546868,
                Telefono = 1145474847,
            };
            var resultadoNewEmpleado = await _userManager.CreateAsync(empleado1, Configs.PasswordDef);
            if (resultadoNewEmpleado.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado1, Configs.EmpleadoRolName);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddOtherEmpleadosAsync()
        {
            Empleado empleado2 = new Empleado()
            {
                Id = 2,
                UserName = "santiagor@ort.edu.ar",
                Password = Configs.PasswordDef,
                Email = "santiagor@ort.edu.ar",
                Nombre = "Santiago",
                Apellido = "Rinaudo",
                DNI = 29344727,
                Telefono = 132550299,
            };
            var resultadoNewEmpleado = await _userManager.CreateAsync(empleado2, Configs.PasswordDef);
            if (resultadoNewEmpleado.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado2, Configs.EmpleadoRolName);
                await _context.SaveChangesAsync();
            }
        }

        private async Task addEpisodion()
        {
            Episodio episodio = new Episodio()
            {
                Id = 27,
                Motivo = "Suba de la presión arterial.",
                Descripcion = "Parámetros del corazon alterados.",
                HistoriaClinicaId = 1212,
                EmpleadoId= 1
            };

            Episodio episodioCerrado = new Episodio()
            {
                Id = 28,
                Motivo = "Descenso de la presión arterial.",
                Descripcion = "Poca presion sanguinea.",
                HistoriaClinicaId = 1213,
                FechaYHoraCierre = DateTime.Now,
                EstadoAbierto = false,
                EmpleadoId = 1

            };

            _context.Add(episodio);
            _context.Add(episodioCerrado);

            await _context.SaveChangesAsync();
        }
            private async Task addEvolucion()
        {
            Evolucion evolucion = new Evolucion()
            {
                Id = 12,
                MedicoId = 40,
                EpisodioId = 28,
                DescripcionAtencion = "El paciente evoluciona favorablemente.",
                FechaYHoraCierre= DateTime.Now,
                EstadoAbierto = false

            };

            _context.Add(evolucion);
            await _context.SaveChangesAsync();
            
        }

        private async Task addEpicrisis()
        {
            Epicrisis epicrisis = new Epicrisis()
            {
                Id = 22,
                MedicoId = 40,
                EpisodioId = 28,
                Descripcion = "Arteria femoral obstruida.",
                Recomendacion = "Valcote 35mg cada 12 hs"

            };

            _context.Add(epicrisis);
            await _context.SaveChangesAsync();

        }

        private async Task addNota()
        {
            Notas nota = new Notas()
            {
                Id = 33,
                EvolucionId = 12,
                EmpleadoId = 1,
                Mensaje = "Arteria femoral obstruida.",
                FechaYHora = DateTime.Now

            };

            _context.Add(nota);
            await _context.SaveChangesAsync();

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
