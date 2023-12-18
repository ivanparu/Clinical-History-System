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
    //[Authorize(Roles = "empleado")]
    [Authorize]
    public class NotasController : Controller
    {
        private readonly HistoriasClinicasCContext _context;
        private readonly UserManager<Persona> _userManager;

        public NotasController(HistoriasClinicasCContext context, UserManager<Persona> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var historiasClinicasCContext = _context.Notas.Include(n => n.Empleado).Include(n => n.Evolucion);
            return View(await historiasClinicasCContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas
                .Include(n => n.Empleado)
                .Include(n => n.Evolucion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // GET: Notas/Create

        [Authorize(Roles = Configs.MedicoRolName + "," + Configs.EmpleadoRolName)]
        public IActionResult Create(int? evolucionId)
        {
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido");
            //ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "DescripcionAtencion");
            if (evolucionId == null)
            {
                //afuera
                return Content("definir que hacemos");
            }
            else
            {
                Notas notas = new Notas();
                //notas.EvolucionId = (int)id;
                TempData["EvolucionId"] = (int)evolucionId;
            }
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.MedicoRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> Create([Bind("Id,Mensaje")] Notas notas)
        {
            notas.EvolucionId = (int)TempData["EvolucionId"];
            var empleadoId = Int32.Parse(_userManager.GetUserId(User));
            notas.EmpleadoId = empleadoId;
            if (ModelState.IsValid)
            {
                _context.Notas.Add(notas);
                await _context.SaveChangesAsync();
                //return RedirectToAction("Index", "Pacientes");
                var evolucion = await _context.Evoluciones.FindAsync(notas.EvolucionId);

                var episodio = await _context.Episodios.FirstOrDefaultAsync(m => m.Id == evolucion.EpisodioId);
                var hc = _context.HistoriaClinicas.Find(episodio.HistoriaClinicaId);
                var pacienteId = hc.PacienteId;
                return RedirectToAction("Details", "Pacientes", new { id = pacienteId });
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", notas.EmpleadoId);
            //ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "DescripcionAtencion", notas.EvolucionId);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", notas.EmpleadoId);
            //ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "DescripcionAtencion", notas.EvolucionId);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EvolucionId,EmpleadoId,Mensaje,FechaYHora")] Notas notas)
        {
            if (id != notas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Notas.Update(notas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasExists(notas.Id))
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
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", notas.EmpleadoId);
            //ViewData["EvolucionId"] = new SelectList(_context.Evoluciones, "Id", "DescripcionAtencion", notas.EvolucionId);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas
                .Include(n => n.Empleado)
                .Include(n => n.Evolucion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notas == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Notas'  is null.");
            }
            var notas = await _context.Notas.FindAsync(id);
            if (notas != null)
            {
                _context.Notas.Remove(notas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasExists(int id)
        {
          return _context.Notas.Any(e => e.Id == id);
        }
    }
}
