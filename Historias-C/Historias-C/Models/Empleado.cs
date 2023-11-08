using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Empleado : Persona
    {

        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(40, MinimumLength = 2, ErrorMessage = ErrorMessages._reqStrMinMax)]
        public string Legajo { get; set; } = Guid.NewGuid().ToString();
       



        public List<Episodio> Episodios { get; set; }   
        
        public List<Notas> Notas { get; set; }
        
    }
}
