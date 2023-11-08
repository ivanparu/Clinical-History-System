using Historias_C.ViewModels;
using Historias_C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

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
        public IActionResult Registrar()
        {
            return View();
        }

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
    }
}
