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
using System.Reflection.Metadata;

namespace Historias_C.Controllers
{
    [Authorize]
    public class DireccionesController : Controller
    {
        private readonly HistoriasClinicasCContext _context;

        public DireccionesController(HistoriasClinicasCContext context)
        {
            _context = context;
        }

        private IEnumerable<Persona> GetPersonasSinDireccion()
        {
            return _context.Personas.Include(p => p.Direccion).Where(p => p.Direccion == null);
        }

        // GET: Direcciones
        public async Task<IActionResult> Index()
        {
            var historiasClinicasCContext = _context.Direcciones.Include(d => d.Persona);
            return View(await historiasClinicasCContext.ToListAsync());
        }

        // GET: Direcciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // GET: Direcciones/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(GetPersonasSinDireccion(), "Id", "Apellido");
            return View();
        }

        // POST: Direcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Calle,Altura,Barrio,Ciudad,PersonaId")] Direccion direccion)
        {
            string errorNoEsperado = string.Empty;

            if (DireccionExists(direccion.Id))
            {
                ModelState.AddModelError("Id", "Esta persona ya tiene una direccion asociada");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(direccion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch(DbUpdateException e)
            {
                SqlException ie = e.InnerException as SqlException;

                if(ie != null && (ie.Number == 2627 || ie.Number == 2601))
                {
                    ModelState.AddModelError("Id", "Esta persona ya tiene una direccion asociada");
                }
                else
                {
                    errorNoEsperado = $"Error no esperado al actualizar la DB: {ie.Message}";
                }
            }
            catch(Exception e)
            {
                errorNoEsperado = $"Error no esperado: {e.InnerException.Message}";
            }

            if (!string.IsNullOrEmpty(errorNoEsperado))
            {
                ModelState.AddModelError(string.Empty, errorNoEsperado);
            }

            
            ViewData["PersonaId"] = new SelectList(GetPersonasSinDireccion(), "Id", "Apellido", direccion.PersonaId);
            return View(direccion);
        }

        // GET: Direcciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(GetPersonasSinDireccion(), "Id", "Apellido", direccion.PersonaId);
            return View(direccion);
        }

        // POST: Direcciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Calle,Altura,Barrio,Ciudad,PersonaId")] Direccion direccion)
        {
            if (id != direccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.Id))
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
            ViewData["PersonaId"] = new SelectList(GetPersonasSinDireccion(), "Id", "Apellido", direccion.PersonaId);
            return View(direccion);
        }

        // GET: Direcciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // POST: Direcciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Direcciones == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Direcciones'  is null.");
            }
            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion != null)
            {
                _context.Direcciones.Remove(direccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
          return _context.Direcciones.Any(e => e.Id == id);
        }
    }
}
