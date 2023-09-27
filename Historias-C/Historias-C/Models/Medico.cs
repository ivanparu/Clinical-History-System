namespace Historias_C.Models
{
    public class Medico : Empleado
    {

        public string Matricula { get; set; }
        public string Especialidad { get; set; }
        public Medico(string nombre, string apellido, int dni, int telefono, Direccion direccion, int id,string matricula, string especialidad) : base(nombre, apellido, dni, telefono, direccion, id)
        {
            this.Matricula = matricula;
            this.Especialidad = especialidad;
        }

        public List<Evolucion> Evoluciones { get; set; }
        public int EvolucionId { get; set; }
        public List<Epicrisis> Epicrisis { get; set;}
        public int EpicrisisId { get; set;}
    }
}
