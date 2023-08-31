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

        
    }

}
