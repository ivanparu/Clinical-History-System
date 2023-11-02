using Historias_C.ViewModels;
using Historias_C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Historias_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona>  _userManager;
        public AccountController(UserManager<Persona> userManager) {
            this._userManager = userManager;
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
                    return RedirectToAction("Edit", "Clientes", new {id = paciente.Id});
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
