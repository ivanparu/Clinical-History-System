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
        public IActionResult Create(int? pacienteId)
        {
            if (pacienteId == null)
            {
                // Manejo del caso donde no se proporciona un ID de paciente
                return RedirectToAction("IndexDePaciente", "Pacientes"); 
            }

            // Se obtiene el paciente correspondiente al ID proporcionado
            var paciente = _context.Pacientes.FirstOrDefault(p => p.Id == pacienteId);

            if (paciente == null)
            {
                // Manejo del caso donde el paciente no existe
                return RedirectToAction("IndexDePaciente", "Pacientes"); 
            }

            ViewData["PersonaId"] = paciente.Id; 
            return View();
        }

        // POST: Direcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Calle,Altura,Barrio,Ciudad,PersonaId")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                // Obtener el paciente actual (suponiendo que tienes el ID del paciente en algún lugar)
                var paciente = _context.Pacientes.FirstOrDefault(p => p.Id == PacienteId);

                // Verificar que el paciente existe
                if (paciente != null)
                {
                    // Establecer el ID del paciente en la dirección
                    direccion.PersonaId = paciente.Id;

                    // Agregar y guardar cambios en la base de datos
                    _context.Add(direccion);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar el caso donde el paciente no existe
                    return RedirectToAction("IndexDePaciente", "Pacientes");
                }
            }

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
    }
}
