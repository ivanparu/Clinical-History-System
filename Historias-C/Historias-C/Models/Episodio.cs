using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Episodio
    {
        private const string _reqMsg = "El campo {0} es requerido.";

        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = _reqRange)]
        public string Motivo { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(400, MinimumLength = 5, ErrorMessage = _reqRange)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraInicio { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraCierre { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraAlta { get; set; }

        public bool EstadoAbierto { get; set; }

        public List<Evolucion> Evoluciones { get; set;}
        public Epicrisis Epicrisis { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        public Empleado EmpleadoRegistra { get; set; }

        public Episodio(string motivo, string descripcion, DateTime fechaYHoraInicio, DateTime fechaYHoraCierre, DateTime fechaYHoraAlta, Empleado empleadoRegistra) {
            this.Motivo = motivo;
            this.Descripcion = descripcion;
            this.FechaYHoraInicio = fechaYHoraInicio;
            this.FechaYHoraCierre = fechaYHoraCierre;
            this.FechaYHoraAlta = fechaYHoraAlta;
            this.EstadoAbierto = true;
            Evoluciones = new List<Evolucion>();
            this.EmpleadoRegistra = empleadoRegistra;

        }

        public Episodio() { }

        [Required(ErrorMessage = _reqMsg)]
        public HistoriaClinica HistoriaClinica { get; set; }
        public int HistoriaClinicaId { get; set; }
    }

    
}
