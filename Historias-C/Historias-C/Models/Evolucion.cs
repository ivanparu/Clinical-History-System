namespace Historias_C.Models
{
    public class Evolucion
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public string DescripcionAtencion { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Notas> Notas { get; set; }
    }



    }

