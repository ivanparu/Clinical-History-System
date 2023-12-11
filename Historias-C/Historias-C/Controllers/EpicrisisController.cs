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
    [Authorize(Roles = Configs.MedicoRolName)]

    public class EpicrisisController : Controller
    {
        private readonly HistoriasClinicasCContext _context;
        private readonly UserManager<Persona> _userManager;

        public EpicrisisController(HistoriasClinicasCContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager; 
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
        [Authorize(Roles = Configs.MedicoRolName)]
        public IActionResult Create(int? episodioId)
        {
            if (episodioId == null)
            {
                //afuera
                return Content("definir que hacemos");
            }
            else
            {
                Epicrisis epicrisis = new Epicrisis();
                //epicrisis.EpisodioId = (int)episodioId;
                TempData["EpisodioId"] = episodioId;
            }
            return View();
        }

        // POST: Epicrisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.MedicoRolName)]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,MedicoId,Recomendacion")] Epicrisis epicrisis)
        {
            

            if (ModelState.IsValid)
            {
                epicrisis.EpisodioId = (int)TempData["EpisodioId"];
                var medicoId = Int32.Parse(_userManager.GetUserId(User));
                epicrisis.MedicoId = medicoId;

                _context.Epicrisis.Add(epicrisis);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Pacientes");
            }
            //ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            //ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", epicrisis.MedicoId);
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
            //ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            //ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", epicrisis.MedicoId);
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
                    _context.Epicrisis.Update(epicrisis);
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
            //ViewData["EpisodioId"] = new SelectList(_context.Episodios, "Id", "Descripcion", epicrisis.EpisodioId);
            //ViewData["MedicoId"] = new SelectList(_context.Medicos, "Id", "Apellido", epicrisis.MedicoId);
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
