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
        [Display(Name = "Medico")]
        public int MedicoId { get; set; }


        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaYHoraInicio { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de alta")]
        public DateTime? FechaYHoraAlta { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de cierre")]
        public DateTime? FechaYHoraCierre { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = _reqRange)]
        public string DescripcionAtencion { get; set; }

        [Display(Name = "Estado")]
        public bool EstadoAbierto { get; set; } = true;

        public List<Notas> Notas { get; set; }


        public Episodio Episodio { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [Display(Name = "Episodio")]
        public int EpisodioId { get; set; }
    }

    

}
