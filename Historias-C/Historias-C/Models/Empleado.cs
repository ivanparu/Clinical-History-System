namespace Historias_C.Models
{
    public class Empleado : Persona
    {
        public string Legajo { get; set; }
        public Empleado(string nombre, string apellido, int dni, int telefono, string direccion, string legajo)
            : base(nombre, apellido, dni, telefono, direccion)
        {
            this.Legajo = legajo;
        }

    }
}
