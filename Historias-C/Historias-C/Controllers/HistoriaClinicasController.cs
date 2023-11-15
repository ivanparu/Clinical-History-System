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
using Microsoft.Data.SqlClient;

namespace Historias_C.Controllers
{
    [Authorize]

    public class HistoriaClinicasController : Controller
    {
        private readonly HistoriasClinicasCContext _context;

        public HistoriaClinicasController(HistoriasClinicasCContext context)
        {
            _context = context;
        }
        private IEnumerable<Paciente> GetPersonasSinHistoria()
        {
            return _context.Pacientes.Include(p => p.HistoriaClinica).Where(p => p.HistoriaClinica == null);
        }

        // GET: HistoriaClinicas
        public async Task<IActionResult> Index()
        {
            var historiasClinicasCContext = _context.HistoriaClinicas.Include(h => h.Paciente);
            return View(await historiasClinicasCContext.ToListAsync());
        }

        // GET: HistoriaClinicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HistoriaClinicas == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriaClinicas
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(GetPersonasSinHistoria(), "Id", "Apellido");
            return View();
        }

        // POST: HistoriaClinicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {

            string errorNoEsperado = string.Empty;

            if (HistoriaClinicaExists(historiaClinica.Id))
            {
                ModelState.AddModelError("Id", "Esta persona ya tiene una historia clinica asociada");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(historiaClinica);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException e)
            {
                SqlException ie = e.InnerException as SqlException;

                if (ie != null && (ie.Number == 2627 || ie.Number == 2601))
                {
                    ModelState.AddModelError("Id", "Esta persona ya tiene una historia clinica asociada");
                }
                else
                {
                    errorNoEsperado = $"Error no esperado al actualizar la DB: {ie.Message}";
                }
            }
            catch (Exception e)
            {
                errorNoEsperado = $"Error no esperado: {e.InnerException.Message}";
            }

            if (!string.IsNullOrEmpty(errorNoEsperado))
            {
                ModelState.AddModelError(string.Empty, errorNoEsperado);
            }
           
            ViewData["PacienteId"] = new SelectList(GetPersonasSinHistoria(), "Id", "Apellido", historiaClinica.PacienteId);
            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HistoriaClinicas == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);
            if (historiaClinica == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(GetPersonasSinHistoria(), "Id", "Apellido", historiaClinica.PacienteId);
            return View(historiaClinica);
        }

        // POST: HistoriaClinicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historiaClinica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriaClinicaExists(historiaClinica.Id))
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
            ViewData["PacienteId"] = new SelectList(GetPersonasSinHistoria(), "Id", "Apellido", historiaClinica.PacienteId);
            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HistoriaClinicas == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriaClinicas
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // POST: HistoriaClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HistoriaClinicas == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.HistoriaClinicas'  is null.");
            }
            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);
            if (historiaClinica != null)
            {
                _context.HistoriaClinicas.Remove(historiaClinica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaClinicaExists(int id)
        {
          return _context.HistoriaClinicas.Any(e => e.Id == id);
        }
    }
}
