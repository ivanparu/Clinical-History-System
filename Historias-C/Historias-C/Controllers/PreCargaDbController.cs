using Historias_C.Data;
using Historias_C.Models;
using Microsoft.AspNetCore.Mvc;

namespace Historias_C.Controllers
{
    public class PreCargaDbController : Controller
    {

        private readonly HistoriasClinicasCContext _context;

        public PreCargaDbController(HistoriasClinicasCContext context)
        {
            this._context = context;
        }
        public IActionResult Seed()
        {

            if(!_context.Personas.Any())
            {
                AddPersonas();
            }

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



            return RedirectToAction("Index", "Home", new {mensaje = "se ejecutó la pre-carga"});
        }

        private void AddMedicos()
        {
            Medico medico1 = new Medico()
            {
                Especialidad = Especialidad.ALERGIA_E_INMUNOLOGIA,
                Matricula = "S1212NM",
                UserName = "joacom",
                Password = "7878sd",
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
                Password = "45457878",
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
                Password = "987654",
                Email = "mariom@ort.edu.ar",
                Nombre = "Mario",
                Apellido = "Martinez",
                DNI = 54546868,
                Telefono = 1145474847,
            };
            _context.Empleados.Add(empleado1);
            _context.SaveChanges();
        }

        public IActionResult Recreate() {
            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();  

            return RedirectToAction("Index", "Home", new { mensaje = "se regeneró la base de datos" });
        }

        private void AddPersonas()
        {
            Persona persona1 = new Persona()
            {
                UserName = "juanperez",
                Password = "12345",
                Email = "juanp@ort.edu.ar",
                Nombre = "Juan",
                Apellido = "Perez",
                DNI = 1234567,
                Telefono = 1155554444,
            };
            _context.Personas.Add(persona1);
            _context.SaveChanges();
        }
    }
}
