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
    public class DiagnosticosController : Controller
    {
        private readonly HistoriasClinicasCContext _context;

        public DiagnosticosController(HistoriasClinicasCContext context)
        {
            _context = context;
        }

        // GET: Diagnosticos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Diagnosticos.ToListAsync());
        }

        // GET: Diagnosticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticos/Create
        public IActionResult Create(int idEpicrisis, string descripcion, string recomendacion)
        {

             Diagnostico diagnostico = new Diagnostico();
             diagnostico.IdEpicrisis = idEpicrisis;
             diagnostico.Descripcion = descripcion;
             diagnostico.Recomendacion = recomendacion;
             
              _context.Diagnosticos.Add(diagnostico);
            
            return View();
        }

        // POST: Diagnosticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEpicrisis,Descripcion,Recomendacion")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnostico);
        }

        // GET: Diagnosticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            return View(diagnostico);
        }

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEpicrisis,Descripcion,Recomendacion")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Id))
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
            return View(diagnostico);
        }

        // GET: Diagnosticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagnosticos == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Diagnosticos'  is null.");
            }
            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnosticos.Remove(diagnostico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
          return _context.Diagnosticos.Any(e => e.Id == id);
        }
    }
}
