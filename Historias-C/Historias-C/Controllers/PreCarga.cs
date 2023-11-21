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
        public IActionResult Seed()
        {
            CrearRoles().Wait();

            if (!_context.Empleados.Any())
            {
                AddEmpleados();
            }

            if (!_context.Pacientes.Any())
            {
                AddPacientes();
            }

            if (!_context.Medicos.Any())
            {
                AddMedicos();
            }


            return RedirectToAction("Index", "Home", new { mensaje = "se ejecutó la pre-carga" });
        }

        private void AddMedicos()
        {
            Medico medico1 = new Medico()
            {
                Especialidad = Especialidad.ALERGIA_E_INMUNOLOGIA,
                Matricula = "S1212NM",
                UserName = "joacom",
                Password = Configs.PasswordDef,
                Email = "joacom@ort.edu.ar",
                Nombre = "Joaco",
                Apellido = "Messi",
                DNI = 12457896,
                Telefono = 1145568978,
            };
            _context.Medicos.Add(medico1);
            _context.SaveChanges();
        }

        private void AddPacientes()
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
            _context.Pacientes.Add(paciente1);
            _context.SaveChanges();
        }

        private void AddEmpleados()
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
            _context.Empleados.Add(empleado1);
            _context.SaveChanges();
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
