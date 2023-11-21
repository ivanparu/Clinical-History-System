using Historias_C.ViewModels;
using Historias_C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Historias_C.Data;
using System.Security.Claims;
using Historias_C.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Historias_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona>  _userManager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasCContext _contexto;
       

        public AccountController(UserManager<Persona> userManager, SignInManager<Persona> signInManager, RoleManager<Rol> roleManager, HistoriasClinicasCContext _contexto) {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._contexto = _contexto;
        }

        [AllowAnonymous]
        public IActionResult Registrar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Email", "Password", "ConfirmPassword")]RegistrarVM model)
        {
            if(ModelState.IsValid)
            {

                Paciente paciente = new Paciente()
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var resultado = await _userManager.CreateAsync(paciente, model.Password);

                if (resultado.Succeeded)
                {
                    //le agrego el rol
                    var resultadoAddRole = await _userManager.AddToRoleAsync(paciente, Configs.PacienteRolName);

                    if (resultadoAddRole.Succeeded)
                    {
                        //creamos la HistoriaClinica y la asocio al paciente creado
                        HistoriaClinica hc = new HistoriaClinica()
                        {
                            PacienteId = paciente.Id,
                        };
                        _contexto.Add(hc);
                        await _contexto.SaveChangesAsync();
                        await _signInManager.SignInAsync(paciente, isPersistent: false);
                        return RedirectToAction("Edit", "Pacientes", new { id = paciente.Id });
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, $"No se pudo agregar el rol de {Configs.PacienteRolName}");
                    }
                    
                }

                foreach(var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult IniciarSesion(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login model)
        {

            string returnUrl = TempData["ReturnUrl"] as string;

            if (ModelState.IsValid)
            {
               var resultado =  await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Recordarme, false);

                if (resultado.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl)) 
                    {
                        return Redirect(returnUrl); 
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(String.Empty, "Inicio de sesion invalido");
            
            }


            return View(model);
        }

        public async Task<IActionResult> CerrarSesion()
        {

           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }


        public IActionResult AccesoDenegado(String returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        public IActionResult TestCurrentUser()
        {

            if (_signInManager.IsSignedIn(User))
            {
                string nombreUsuario = User.Identity.Name;

                Persona persona = _contexto.Personas.FirstOrDefault(p => p.NormalizedUserName == nombreUsuario.ToUpper());

            }
            return null;
        }

        [HttpGet]

        public async Task<IActionResult> EmailDisponible(string email)
        {
            var PersonaExistente = await _userManager.FindByEmailAsync(email);

            if(PersonaExistente == null)
            {
                return Json(true);// no hay persona con ese email
            }
            else
            {
                return Json($"El correo {email} ya está en uso"); //el email ya esta en uso
            }
        }

    }

}

