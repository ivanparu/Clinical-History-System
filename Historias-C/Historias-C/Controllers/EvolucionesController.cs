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

namespace Historias_C.Controllers
{
    [Authorize]

    public class EvolucionesController : Controller
    {
        private readonly HistoriasClinicasCContext _context;

        public EvolucionesController(HistoriasClinicasCContext context)
        {
            _context = context;
        }

        // GET: Evoluciones
        public async Task<IActionResult> Index()
        {
            var historiasClinicasCContext = _context.Evoluciones.Include(e => e.Episodio).Include(e => e.Medico);
            return View(await historiasClinicasCContext.ToListAsync());
        }

        // GET: Evoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evoluciones == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Episodio)
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // GET: Evoluciones/Create
        public IActionResult Create(int? episodioId)
        {
            // ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion");
            //ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido");
            if (episodioId == null)
            {
                //afuera
                return Content("definir que hacemos");
            }
            else
            {
                Evolucion evolucion = new Evolucion();
                evolucion.EpisodioId = (int)episodioId;
            }

            return View();
        }

        // POST: Evoluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion,EstadoAbierto,EpisodioId")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", evolucion.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", evolucion.MedicoId);
            return View(evolucion);
        }

        // GET: Evoluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evoluciones == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones.FindAsync(id);
            if (evolucion == null)
            {
                return NotFound();
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", evolucion.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", evolucion.MedicoId);
            return View(evolucion);
        }

        // POST: Evoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,DescripcionAtencion,EstadoAbierto,EpisodioId")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvolucionExists(evolucion.Id))
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
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", evolucion.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", evolucion.MedicoId);
            return View(evolucion);
        }

        // GET: Evoluciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evoluciones == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evoluciones
                .Include(e => e.Episodio)
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // POST: Evoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evoluciones == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Evoluciones'  is null.");
            }
            var evolucion = await _context.Evoluciones.FindAsync(id);
            if (evolucion != null)
            {
                _context.Evoluciones.Remove(evolucion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvolucionExists(int id)
        {
          return _context.Evoluciones.Any(e => e.Id == id);
        }
    }
}
