﻿using System;
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
using Historias_C.Helpers;
using Microsoft.AspNetCore.Identity;
using Historias_C.ViewModels;

namespace Historias_C.Controllers
{
    [Authorize(Roles = Configs.EmpleadoRolName)]

    public class MedicosController : Controller
    {
        private readonly HistoriasClinicasCContext _context;
        private readonly UserManager<Persona> _userManager;

        public MedicosController(HistoriasClinicasCContext context, UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;

        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Medicos.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        [Authorize(Roles = Configs.EmpleadoRolName)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Configs.EmpleadoRolName)]
        public async Task<IActionResult> Create( RegistrarMedico model)
        {
           //  VerificarMatricula(model);

            if (ModelState.IsValid)
            {
                Medico medicoNuevo = new Medico()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    DNI = model.DNI,
                    Telefono = model.Telefono,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Especialidad = model.Especialidad,
                    Matricula = model.Matricula

                };

                if (!VerificarMatricula(medicoNuevo))
                {

                    ModelState.AddModelError(string.Empty, ErrorMessages.MatriculaExistente);
                }

                var resultadoNewMedico = await _userManager.CreateAsync(medicoNuevo, Configs.PasswordDef);
                
                if (resultadoNewMedico.Succeeded)
                {
                    var resultadoAddRole = await _userManager.AddToRoleAsync(medicoNuevo, Configs.MedicoRolName);
                
                    if (resultadoAddRole.Succeeded)
                {
                    try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbex)
                {
                    ProcesoDuplicado(dbex);
                }
                }
                else
                {
                    return Content($"No se ha podido agregar el rol {Configs.MedicoRolName}");
                }
                }
                foreach (var error in resultadoNewMedico.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }
        private void ProcesoDuplicado(DbUpdateException dbex)
        {
            SqlException innerException = dbex.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                ModelState.AddModelError("Matricula", ErrorMessages.MatriculaExistente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, dbex.Message);
            }
        }

        private bool VerificarMatricula(Medico medico)
        {
            bool resultado = true;

            if (MatriculaExist(medico))
            {
                resultado = false;
                ModelState.AddModelError("Matricula", "La matricula ya existe, verificada en BE");
            }
            return resultado;
        }

        private bool MatriculaExist(Medico medico)
        {
            bool resultado = false;
            if (!string.IsNullOrEmpty(medico.Matricula))
            {
                if (medico.Id != null && medico.Id != 0)
                {
                    //Es una modificación, me interesa que по exista solo si no es del registro que soy.
                    resultado = _context.Medicos.Any(m => m.Matricula == medico.Matricula && m.Id != medico.Id);
                }
                else
                {
                    //Es una creación, solo me interesa que no exista la patente
                    resultado = _context.Medicos.Any(m => m.Matricula == medico.Matricula);
                }
            }
            return resultado;
        }


        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            VerificarMatricula(medico);

            if (ModelState.IsValid)
            {
                try
                {
                    var medicoEnDb = _context.Medicos.Find(id);

                    if (medicoEnDb == null)
                    {
                        return NotFound();
                    }

                    medicoEnDb.Matricula = medico.Matricula;
                    medicoEnDb.Especialidad = medico.Especialidad;
                    medicoEnDb.Legajo = medico.Legajo;
                    medicoEnDb.Email = medico.Email;
                    medicoEnDb.UserName = medico.Email;
                    medicoEnDb.Nombre = medico.Nombre;
                    medicoEnDb.Apellido = medico.Apellido;
                    medicoEnDb.DNI = medico.DNI;
                    medicoEnDb.Telefono = medico.Telefono;

                    _context.Medicos.Update(medicoEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(DbUpdateException dbex)
                {
                    ProcesoDuplicado(dbex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicos == null)
            {
                return Problem("Entity set 'HistoriasClinicasCContext.Medicos'  is null.");
            }
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
          return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
