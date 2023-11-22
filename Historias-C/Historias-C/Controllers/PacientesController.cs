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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObraSocial,Id,UserName,Password,Email,FechaAlta,Nombre,Apellido,DNI,Telefono")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {

                paciente.UserName = paciente.Email;
                var resultadoNewPaciente = await _userManager.CreateAsync(paciente,Configs.PasswordDef);

                //creo con usermanager
                //si está ok
                //le agrego el rol
                if (resultadoNewPaciente.Succeeded)
                {
                    var resultadoAddRole = await _userManager.AddToRoleAsync(paciente, Configs.PacienteRolName);
                    if (resultadoAddRole.Succeeded)
                    {
                        HistoriaClinica hc = new HistoriaClinica()
                        {
                            PacienteId = paciente.Id,
                        };
                        _context.Add(hc);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Pacientes");
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

                    _context.Update(pacienteEnDb);
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
