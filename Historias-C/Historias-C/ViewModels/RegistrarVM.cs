using System.ComponentModel.DataAnnotations;

namespace Historias_C.ViewModels
{
    public class RegistrarVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="La {0} no es igual")]
        [Display(Name ="Confirmacion")]
        public string ConfirmPassword { get; set; }
    }
}
