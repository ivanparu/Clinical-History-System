using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_C.Data;
using Historias_C.Models;

namespace Historias_C.Controllers
{
    public class PacientesController : Controller
    {
        private readonly HistoriasClinicasCContext _context;

        public PacientesController(HistoriasClinicasCContext context)
        {
            _context = context;
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
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("ObraSocial,Id,UserName,Password,Email,FechaAlta,Nombre,Apellido,DNI,Telefono")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var pacienteEnDb = _context.Pacientes.Find(paciente.Id);

                    if (pacienteEnDb == null)
                    {
                        return NotFound();
                    }


                    pacienteEnDb.ObraSocial = paciente.ObraSocial;
                    pacienteEnDb.UserName = paciente.UserName;
                    pacienteEnDb.Nombre = paciente.Nombre;
                    pacienteEnDb.Apellido = paciente.Apellido;
                    pacienteEnDb.DNI = paciente.DNI;
                    pacienteEnDb.Telefono = paciente.Telefono;

                    if (!ActualizarEmail(paciente, pacienteEnDb))
                    {
                        ModelState.AddModelError("Email", "El email ya está en uso");
                        return View(paciente);


                    }



                    _context.Update(pacienteEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }


        private bool ActualizarEmail(Paciente paciente, Paciente pacienteendb)
        {
            bool resultado = true;

            try
            {
                if (!pacienteendb.NormalizedEmail.Equals(paciente.Email.ToUpper()))
                {
                    if (EmailExist(paciente.Email))
                    {
                        resultado = false;
                    }
                    else
                    {
                        pacienteendb.Email = paciente.Email;
                        pacienteendb.NormalizedEmail = paciente.Email.ToUpper();
                        pacienteendb.UserName = paciente.Email;
                        pacienteendb.NormalizedUserName = paciente.NormalizedEmail;

                    }

                }
                else
                {
                    //no se actualiza ya que son iguales.
                }




            }
            catch
            {

                resultado = false;


            }

            return resultado;

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

        private bool EmailExist(String email)
        {
            return _context.Pacientes.Any(e => e.Email == email);
        }
    }
}
