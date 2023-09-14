using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Evolucion
    {
        private const string _reqMsg = "El campo {0} es requerido.";

        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";

        public int Id { get; set; }
        public Medico Medico { get; set; }
        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraInicio { get; set; }
        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraAlta { get; set; }
        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraCierre { get; set; }
        [Required(ErrorMessage = _reqMsg)]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = _reqRange)]
        public string DescripcionAtencion { get; set; }
        [Required(ErrorMessage = _reqMsg)]

        public bool EstadoAbierto { get; set; }
        public List<Notas> Notas { get; set; }
    }

    
}
