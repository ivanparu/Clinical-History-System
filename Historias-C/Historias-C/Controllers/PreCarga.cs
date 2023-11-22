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
            }

            if (!_context.Medicos.Any())
            {
                await AddMedicosAsync();
            }


            return RedirectToAction("Index", "Home", new { mensaje = "se ejecutó la pre-carga" });
        }

        private async Task AddMedicosAsync()
        {
            Medico medico1 = new Medico()
            {
                Especialidad = Especialidad.ALERGIA_E_INMUNOLOGIA,
                Matricula = "S1212NM",
                Email = "joacom@ort.edu.ar",
                UserName = "joacom",
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
                ObraSocial = ObraSocial.OSDE,
                UserName = "sergiol",
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
                UserName = "mariomartinez",
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
