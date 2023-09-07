namespace Historias_C.Models
{
    public class Notas
    {
        public int Id { get; set; }
        public Evolucion Evolucion { get; set; }
        public Empleado Empleado { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaYHora { get; set; }
    }
}
