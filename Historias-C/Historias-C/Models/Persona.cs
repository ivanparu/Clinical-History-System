namespace Historias_C.Models
{
    public class Persona
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; } // TO DO: Esta seria una clase aparte. 


        public Persona(string nombre, string apellido, int dni, int telefono, string direccion)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Direccion = direccion;
            this.Password = "Password1!";
            this.UserName = $"{nombre}{apellido}@ort.edu.ar";
            this.Email = $"{nombre}{apellido}@ort.edu.ar";
            this.FechaAlta = DateTime.Now;

        }
    }

}
