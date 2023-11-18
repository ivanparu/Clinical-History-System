using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_C.Models
{
    public class Episodio
    {
        private const string _reqMsg = "El campo {0} es requerido.";

        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";

        public int Id { get; set; }
        [Required(ErrorMessage = _reqMsg)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = _reqRange)]
        public string Motivo { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(400, MinimumLength = 5, ErrorMessage = _reqRange)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaYHoraInicio { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de cierre")]
        public DateTime? FechaYHoraCierre { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de alta")]
        public DateTime? FechaYHoraAlta { get; set; }

        [Display(Name = "Estado")]
        public bool EstadoAbierto { get; set; } = true;

        public List<Evolucion> Evoluciones { get; set;}
        public Epicrisis Epicrisis { get; set; }

        [ForeignKey("Epicrisis")]
        public int? EpicrisisId { get; set; }

        [Required(ErrorMessage = _reqMsg)]

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; } //empleado que registra el episodio
        public Empleado Empleado { get; set; }



        [Display(Name = "Historia Clinica")]
        public HistoriaClinica HistoriaClinica { get; set; }

        [ForeignKey("HistoriaClinica")]
        
        public int HistoriaClinicaId { get; set; }
    }

    
}
