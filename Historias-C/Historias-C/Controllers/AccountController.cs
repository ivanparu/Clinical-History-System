using Historias_C.ViewModels;
using Historias_C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Historias_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona>  _userManager;
        private readonly SignInManager<Persona> _signInManager;

        public AccountController(UserManager<Persona> userManager, SignInManager<Persona> signInManager) {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Registrar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarVM model)
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
                    await _signInManager.SignInAsync(paciente, isPersistent: false);
                    return RedirectToAction("Edit", "Pacientes", new {id = paciente.Id});
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

    }



}
