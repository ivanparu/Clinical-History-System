using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Medico : Empleado
    {
        [Required(ErrorMessage = ErrorMessages._reqMsg)]

        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMessages._reqStrMinMax)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        public Especialidad Especialidad { get; set; } 

        

        public List<Evolucion> Evoluciones { get; set; }
        
        public List<Epicrisis> Epicrisis { get; set;}
        
    }
}
