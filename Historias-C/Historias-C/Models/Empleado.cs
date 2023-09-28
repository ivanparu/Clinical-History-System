using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Empleado : Persona
    {

        [Required(ErrorMessage = ErrorMessages._reqMsgDiagnostico)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMessages._reqStrMinMaxDiagnostico)]
        public string Legajo { get; set; }

        [Required(ErrorMessage = ErrorMessages._reqMsgDiagnostico)]
        public Direccion Direccion { get; set; }

        public Empleado(string nombre, string apellido, int dni, int telefono, Direccion direccion, int id)
            : base(nombre, apellido, dni, telefono, direccion, id)
        {
            this.Legajo = $"{id}-{dni}";
        }

        public Empleado() { }
        public List<Episodio> Episodios { get; set; }   
        public int EpisodioId { get; set; }
        public List<Notas> Notas { get; set; }
        public int NotaId { get; set; }
    }
}
