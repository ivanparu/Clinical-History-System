using Historias_C.Data;
using Historias_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Historias_C.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasCContext _context;


        private List<string> roles = new List<string>() { "Admin", "Empleado", "Paciente" };
        public PreCarga(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistoriasClinicasCContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }
        public IActionResult Seed()
        {
            CrearRoles().Wait();



            return RedirectToAction("Index", "Home", new { mensaje = "se ejecutó la pre-carga" });
        }
        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                   await _roleManager.CreateAsync(new Rol(rolName));

                }
            }
        }
    }
}
