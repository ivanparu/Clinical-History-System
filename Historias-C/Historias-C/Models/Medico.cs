using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Medico : Empleado
    {
        [Required(ErrorMessage = ErrorMessages._reqMsg)]

        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMessages._reqStrMinMax)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        public string Especialidad { get; set; } //Enum??
        public Medico(string nombre, string apellido, int dni, int telefono, Direccion direccion, int id,string matricula, string especialidad) : base(nombre, apellido, dni, telefono, direccion, id)
        {
            this.Matricula = matricula;
            this.Especialidad = especialidad;
        }
        public Medico() { }

        public List<Evolucion> Evoluciones { get; set; }
        public int EvolucionId { get; set; }
        public List<Epicrisis> Epicrisis { get; set;}
        public int EpicrisisId { get; set;}
    }
}
