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
                return View(); 
            }
        }

        // GET: Pacientes
        public async Task<IActionResult> IndexDePaciente(int? id)
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
        public async Task<IActionResult> Create([Bind("ObraSocial,Id,UserName,Password,Email,FechaAlta,Nombre,Apellido,DNI,Telefono")] Paciente paciente)
        {

            VerificarDNI(paciente); 

            if (ModelState.IsValid)
            {

                paciente.UserName = paciente.Email;
                paciente.FechaAlta = DateTime.Now;
                var resultadoNewPaciente = await _userManager.CreateAsync(paciente,Configs.PasswordDef);

                //creo con usermanager
                //si está ok
                //le agrego el rol
                if (resultadoNewPaciente.Succeeded)
                {
                    var resultadoAddRole = await _userManager.AddToRoleAsync(paciente, Configs.PacienteRolName);
                    if (resultadoAddRole.Succeeded)
                    {

                        try
                        {
                            HistoriaClinica hc = new HistoriaClinica()
                            {
                                PacienteId = paciente.Id,
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
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(paciente);
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

        private void VerificarDNI(Paciente paciente)
        {
            if (DNIExist(paciente))
            {
                ModelState.AddModelError("DNI", "El DNI ya existe, verificada en BE");
            };
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
        public async Task<IActionResult> Edit(int id, [Bind("ObraSocial,Id,UserName,Password,Email,FechaAlta,Nombre,Apellido,DNI,Telefono")] Paciente pacienteDelFormulario)
        {
            if (id != pacienteDelFormulario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pacienteEnDb = _context.Pacientes.Find(pacienteDelFormulario.Id);
                    if(pacienteEnDb == null)
                    {
                        return NotFound();
                    }
                    pacienteEnDb.ObraSocial = pacienteDelFormulario.ObraSocial;
                    pacienteEnDb.FechaAlta = pacienteDelFormulario.FechaAlta;
                    pacienteEnDb.Nombre = pacienteDelFormulario.Nombre;
                    pacienteEnDb.Apellido = pacienteDelFormulario.Apellido;
                    pacienteEnDb.Telefono = pacienteDelFormulario.Telefono;
                    pacienteEnDb.DNI = pacienteDelFormulario.DNI;

                    if(ActualizarEmail(pacienteDelFormulario, pacienteEnDb))
                    {
                        ModelState.AddModelError("Email", "El email ya está en uso");
                        return View(pacienteDelFormulario);
                    }

                    _context.HistoriaClinicas.Update(pacienteEnDb);
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
                if (pacienteEnDb.NormalizedEmail.Equals(pacienteDelFormulario.Email.ToUpper()))
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
