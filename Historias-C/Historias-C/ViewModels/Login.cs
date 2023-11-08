using System.ComponentModel.DataAnnotations;

namespace Historias_C.ViewModels
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool Recordarme { get; set; }    
    }
}
