using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Historias_C.ViewModels
{
    public class RegistrarVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        [Remote(action:"EmailDisponible", controller:"Account")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="La contraseña no es igual")]
        [Display(Name ="Confirmar contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
