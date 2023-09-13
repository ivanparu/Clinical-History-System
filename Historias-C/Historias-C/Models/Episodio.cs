namespace Historias_C.Models
{
    public class Episodio
    {

        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Evolucion> Evoluciones { get; set;}
        public Epicrisis Epicrisis { get; set; }
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
    }
}
