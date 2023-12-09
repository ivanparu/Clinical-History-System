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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Historias_C.Helpers;

namespace Historias_C.Controllers
{
    [Authorize]

    public class EpisodiosController : Controller
    {
        private readonly HistoriasClinicasCContext _context;
        private readonly UserManager<Persona> _userManager;

        public EpisodiosController(HistoriasClinicasCContext context,UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: Episodios
        public async Task<IActionResult> Index()
        {
            var historiasClinicasCContext = _context.Episodios.Include(e => e.Empleado).Include(e => e.Epicrisis).Include(e => e.HistoriaClinica);
            return View(await historiasClinicasCContext.ToListAsync());
        }

        // GET: Episodios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Episodios == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .Include(e => e.Empleado)
                .Include(e => e.Epicrisis)
                .Include(e => e.HistoriaClinica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // GET: Episodios/Create
        [Authorize(Roles = Configs.EmpleadoRolName)]
        public IActionResult Create(int? pacienteId)
        {
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido");
            //ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Descripcion");
            //ViewData["HistoriaClinicaId"] = new SelectList(_context.HistoriaClinicas, "Id", "Id");
            if(pacienteId == null)
            {
                //afuera
                return Content("definir que hacemos");
            }

           

            int? hcId = GetHC(pacienteId.Value);

            if(hcId == null)
            {
                //no puedo avanzar
                return Content("el paciente no tiene historia clinica");
            }

            Episodio episodio = new Episodio();
            episodio.HistoriaClinicaId = hcId.Value;


            return View(episodio);
        }

        private int? GetHC(int id)
        {
            int? hc =null;
            var paciente = _context.Pacientes.Include(p=> p.HistoriaClinica).FirstOrDefault(p=>p.Id == id);
            if(paciente != null && paciente.HistoriaClinica != null)
            {
                hc = paciente.HistoriaClinica.Id;
            }
            return hc;
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.EmpleadoRolName)]
        public async Task<IActionResult> Create([Bind("Id,Motivo,Descripcion,FechaYHoraInicio,EstadoAbierto,HistoriaClinicaId")] Episodio episodio)
        {


            if (ModelState.IsValid)
            {
                //Identificamos al empleado que esta ejecutando la aplicación y se lo asigno a episodio.empleadoid
                var empleadoId = Int32.Parse(_userManager.GetUserId(User));
                episodio.EmpleadoId = empleadoId;

                _context.Episodios.Add(episodio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Pacientes");
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoId);
           //ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Descripcion", episodio.EpicrisisId);
            //ViewData["HistoriaClinicaId"] = new SelectList(_context.HistoriaClinicas, "Id", "Id", episodio.HistoriaClinicaId);
            return RedirectToAction("Index", "Pacientes");
        }

        // GET: Episodios/Edit/5
        [Authorize(Roles = Configs.MedicoRolName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Episodios == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios.FindAsync(id);
            if (episodio == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoId);
            ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Descripcion", episodio.EpicrisisId);
            ViewData["HistoriaClinicaId"] = new SelectList(_context.HistoriaClinicas, "Id", "Id", episodio.HistoriaClinicaId);
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.MedicoRolName)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Motivo,Descripcion,FechaYHoraInicio,FechaYHoraCierre,FechaYHoraAlta,EstadoAbierto,EpicrisisId,EmpleadoId,HistoriaClinicaId")] Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }
            if (episodio.EstadoAbierto == true)
            {
                ModelState.AddModelError("EstadoAbierto", "Si se quiere cerrar el episodio, esto debe estar destildado");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    episodio.FechaYHoraCierre = DateTime.Now;
                    _context.Episodios.Update(episodio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Create", "Epicrisis", episodio.Id);
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Apellido", episodio.EmpleadoId);
            //ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Descripcion", episodio.EpicrisisId);
            //ViewData["HistoriaClinicaId"] = new SelectList(_context.HistoriaClinicas, "Id", "Id", episodio.HistoriaClinicaId);
            return View(episodio);
        }

        // GET: Episodios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Episodios == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodios
                .Include(e => e.Empleado)
                .Include(e => e.Epicrisis)
                .Include(e => e.HistoriaClinica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Episodios == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Episodios'  is null.");
            }
            var episodio = await _context.Episodios.FindAsync(id);
            if (episodio != null)
            {
                _context.Episodios.Remove(episodio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(int id)
        {
          return _context.Episodios.Any(e => e.Id == id);
        }
    }
}
