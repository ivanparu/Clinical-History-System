using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_C.Data;
using Historias_C.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Historias_C.Helpers;
using Microsoft.Data.SqlClient;
using Historias_C.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Historias_C.Controllers
{
    [Authorize]

    public class PacientesController : Controller
    {
        private readonly HistoriasClinicasCContext _context;
        private readonly UserManager<Persona> _userManager;

        public PacientesController(HistoriasClinicasCContext context, UserManager<Persona> userManager)
        {
            _context = context; 
            this._userManager = userManager;

        }

        [HttpGet("PonerDNI")]
        public IActionResult PonerDNI()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult BuscarDNI(int DNI)
        {
            // Verificar si existe un paciente con el DNI proporcionado
            var paciente = _context.Pacientes.FirstOrDefault(p => p.DNI == DNI);

            if (paciente != null)
            {
                // Redirigir a la acción IndexPaciente y pasar el paciente como modelo
                return RedirectToAction("IndexDePaciente","Pacientes" ,new { id = paciente.Id });
            }
            else
            {
                // Agregar un error de modelo si no se encuentra el paciente
                ModelState.AddModelError("DNI", "No se encontró ningún paciente con ese DNI");
                return View("PonerDNI"); 
            }
        }

        // GET: Pacientes
        public async Task<IActionResult> IndexDePaciente(int? id, int? direccionId)
        {
            if (id == null)
            {
                return NotFound();
            }


            // Obtener el paciente de la base de datos
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            if(direccionId != null)
            {
                var direccion = await _context.Direcciones.FirstOrDefaultAsync(d => d.Id == direccionId);
                paciente.Direccion = direccion;
            }
            // Pasar el paciente a la vista
            return View(paciente);
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
            .Include(p => p.HistoriaClinica)
                 .ThenInclude(hc => hc.Episodios)
                    .ThenInclude(e => e.Evoluciones)
                        .ThenInclude(ev => ev.Notas)
              .Include(p => p.HistoriaClinica)
                 .ThenInclude(hc => hc.Episodios)
                    .ThenInclude(e => e.Evoluciones)
                      .ThenInclude(ep => ep.Medico)
                       .Include(p => p.HistoriaClinica)
        .ThenInclude(hc => hc.Episodios)
            .ThenInclude(e => e.Epicrisis)
            .Include(p => p.HistoriaClinica)
                .ThenInclude(hc => hc.Episodios)
                    .ThenInclude(e => e.Epicrisis)
                        .ThenInclude(ep => ep.Medico)
            .Include(p => p.HistoriaClinica)
                .ThenInclude(hc => hc.Episodios)
                    .ThenInclude(e => e.Empleado)
            .Include(p => p.HistoriaClinica)
                .ThenInclude(hc => hc.Episodios)
                    .ThenInclude(e => e.Empleado)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        [Authorize(Roles = Configs.EmpleadoRolName)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.EmpleadoRolName)]
        public async Task<IActionResult> Create( RegistrarPaciente model)
        {


            if (ModelState.IsValid)
            {

                Paciente pacienteNuevo = new Paciente()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    DNI = model.DNI,
                    ObraSocial = model.ObraSocial,
                    Telefono =  model.Telefono,
                    Nombre=model.Nombre,
                    Apellido=model.Apellido,

                };

                if (!VerificarDNI(pacienteNuevo)){

                 ModelState.AddModelError(string.Empty, ErrorMessages.DNIExistente);
                }


                var resultadoNewPaciente = await _userManager.CreateAsync(pacienteNuevo, Configs.PasswordDef);


                //creo con usermanager
                //si está ok
                //le agrego el rol
                if (resultadoNewPaciente.Succeeded)
                {
                    var resultadoAddRole = await _userManager.AddToRoleAsync(pacienteNuevo, Configs.PacienteRolName);
                    if (resultadoAddRole.Succeeded)
                    {

                        try
                        {
                            HistoriaClinica hc = new HistoriaClinica()
                            {
                                PacienteId = pacienteNuevo.Id,
                            };
                            _context.HistoriaClinicas.Add(hc);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index", "Pacientes");
                        } catch (DbUpdateException dbex)
                        {
                            ProcesoDuplicado(dbex);
                        }

                    }
                    else
                    {
                        return Content($"No se ha podido agregar el rol {Configs.PacienteRolName}");
                    }
                }


                //creamos la HistoriaClinica y la asocio al paciente creado
                foreach(var error in resultadoNewPaciente.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
           // return RedirectToAction("Index", "Empleados");
           return( View(model) );
        }

        private void ProcesoDuplicado(DbUpdateException dbex)
        {
            SqlException innerException = dbex.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                ModelState.AddModelError("DNI", ErrorMessages.DNIExistente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, dbex.Message);
            }
        }

        private bool VerificarDNI(Paciente paciente)
        {
            bool resultado = true;

            if (DNIExist(paciente))
            {
                resultado = false;
                ModelState.AddModelError("DNI", "El DNI ya existe, verificada en BE");
            };
            return resultado;
        }

        private bool DNIExist(Paciente paciente)
        {
             bool resultado = false;

    if (paciente != null && paciente.DNI != 0)
    {
        if (paciente.Id != null && paciente.Id != 0)
        {
            // Es una modificación, me interesa que no exista solo si no es del registro que estoy modificando.
            resultado = _context.Pacientes.Any(p => p.DNI == paciente.DNI && p.Id != paciente.Id);
        }
        else
        {
            // Es una creación, solo me interesa que no exista el DNI.
            resultado = _context.Pacientes.Any(p => p.DNI == paciente.DNI);
        }
    }
            return resultado;
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paciente pacienteDelFormulario)
        {
            if (id != pacienteDelFormulario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pacienteEnDb = _context.Pacientes.Find(id);
                    if(pacienteEnDb == null)
                    {
                        return NotFound();
                    }
                    pacienteEnDb.ObraSocial = pacienteDelFormulario.ObraSocial;
                    pacienteEnDb.Nombre = pacienteDelFormulario.Nombre;
                    pacienteEnDb.Apellido = pacienteDelFormulario.Apellido;
                    pacienteEnDb.Telefono = pacienteDelFormulario.Telefono;
                    pacienteEnDb.DNI = pacienteDelFormulario.DNI;

                    if(!ActualizarEmail(pacienteDelFormulario, pacienteEnDb))
                    {
                        ModelState.AddModelError("Email", "El email ya está en uso");
                        return View(pacienteDelFormulario);
                    }

                    _context.Pacientes.Update(pacienteEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(pacienteDelFormulario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pacienteDelFormulario);
        }

        private bool ActualizarEmail(Paciente pacienteDelFormulario, Paciente pacienteEnDb)
        {
            bool resultado = true;
            try
            {
                if (!pacienteEnDb.NormalizedEmail.Equals(pacienteDelFormulario.Email.ToUpper()))
                {
                    if (ExistEmail(pacienteDelFormulario.Email))
                    {
                        resultado = false;
                    }
                    else
                    {
                        pacienteEnDb.Email = pacienteDelFormulario.Email;
                        pacienteEnDb.NormalizedEmail = pacienteDelFormulario.Email.ToUpper();
                        pacienteEnDb.UserName = pacienteDelFormulario.Email;
                        pacienteEnDb.NormalizedUserName = pacienteDelFormulario.NormalizedEmail;
                    }
                }
                else
                {
                    
                }
            }
            catch
            {
                resultado &= false;
            }
            return resultado;
        }

        private bool ExistEmail(string email)
        {
            return _context.Pacientes.Any(p=>p.NormalizedEmail == email.ToUpper());
                }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacientes == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Pacientes'  is null.");
            }
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
          return _context.Pacientes.Any(e => e.Id == id);
        }  
    }
}
