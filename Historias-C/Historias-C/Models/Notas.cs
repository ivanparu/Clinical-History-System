using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_C.Models
{
    public class Notas
    {
        private const string _reqMsg = "El campo {0} es requerido.";

        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";
        public int Id { get; set; }
        public Evolucion Evolucion { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        public int EvolucionId { get; set; }

        
        public Empleado Empleado { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; } //el empleado que cargo la nota

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(400, MinimumLength =2, ErrorMessage = _reqRange)]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHora { get; set; } = DateTime.Now;
    }
}
