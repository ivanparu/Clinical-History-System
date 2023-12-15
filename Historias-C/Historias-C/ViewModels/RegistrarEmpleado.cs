

using Historias_C.Models;
using System.ComponentModel.DataAnnotations;
using Historias_C.Helpers;
namespace Historias_C.ViewModels
{
    public class RegistrarEmpleado
    { 

            [Required(ErrorMessage = ErrorMessages._reqMsg)]
            [EmailAddress]
            [Display(Name = "Correo electronico")]
            public string Email { get; set; }

            [Required(ErrorMessage = ErrorMessages._reqMsg)]
            [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMessages._reqRange)]
            public string Nombre { get; set; }

            [Required(ErrorMessage = ErrorMessages._reqMsg)]
            [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMessages._reqRange)]
            public string Apellido { get; set; }

            [Required(ErrorMessage = ErrorMessages._reqMsg)]
            [RegularExpression(@"^\d{6,14}$", ErrorMessage = "El DNI debe contener entre 6 y 14 dígitos.")]
            public int DNI { get; set; }


            [Required(ErrorMessage = ErrorMessages._reqMsg)]
            [DataType(DataType.PhoneNumber)]
            public int Telefono { get; set; }


        }
    }