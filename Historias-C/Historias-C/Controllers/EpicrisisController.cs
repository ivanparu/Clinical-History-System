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
    public class EpicrisisController : Controller
    {
        private readonly HistoriasClinicasCContext _context;

        public EpicrisisController(HistoriasClinicasCContext context)
        {
            _context = context;
        }

        // GET: Epicrisis
        public async Task<IActionResult> Index()
        {
            var historiasClinicasCContext = _context.Epicrisis.Include(e => e.Episodio).Include(e => e.Medico);
            return View(await historiasClinicasCContext.ToListAsync());
        }

        // GET: Epicrisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Epicrisis == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .Include(e => e.Episodio)
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // GET: Epicrisis/Create
        public IActionResult Create()
        {
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido");
            return View();
        }

        // POST: Epicrisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EpisodioId,MedicoId,FechaYHora,Descripcion,Recomendacion")] Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epicrisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", epicrisis.MedicoId);
            return View(epicrisis);
        }

        // GET: Epicrisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Epicrisis == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis.FindAsync(id);
            if (epicrisis == null)
            {
                return NotFound();
            }
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", epicrisis.MedicoId);
            return View(epicrisis);
        }

        // POST: Epicrisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EpisodioId,MedicoId,FechaYHora,Descripcion,Recomendacion")] Epicrisis epicrisis)
        {
            if (id != epicrisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epicrisis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpicrisisExists(epicrisis.Id))
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
            ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", epicrisis.MedicoId);
            return View(epicrisis);
        }

        // GET: Epicrisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Epicrisis == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .Include(e => e.Episodio)
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // POST: Epicrisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Epicrisis == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Epicrisis'  is null.");
            }
            var epicrisis = await _context.Epicrisis.FindAsync(id);
            if (epicrisis != null)
            {
                _context.Epicrisis.Remove(epicrisis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpicrisisExists(int id)
        {
          return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
