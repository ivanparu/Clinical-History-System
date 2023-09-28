using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Persona
    {
        private const string _reqMsg = "El campo {0} es requerido.";

        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";
        public int Id { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = _reqRange)]
        public string UserName { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(25, MinimumLength = 8, ErrorMessage = _reqRange)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = _reqRange)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = _reqRange)]
        public string Apellido { get; set; }


        [Required(ErrorMessage = _reqMsg)]
        [RegularExpression(@"^\d{6,14}$", ErrorMessage = "El DNI debe contener entre 6 y 14 dígitos.")]
        public int DNI { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [Phone]
        public int Telefono { get; set; }

        //@TO DO: FALTA PONER en REQUIRE cuando se defina Direccon clase aparte.
      
        public List<Direccion> Direccion { get; set; }

        public Persona(string nombre, string apellido, int dni, int telefono, Direccion direccion, int Id)
        {
            this.Id = Id;
            this.DNI = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Direccion = new List<Direccion>();
            this.Password = "Password1!";
            this.UserName = $"{nombre}{apellido}@ort.edu.ar";
            this.Email = $"{nombre}{apellido}@ort.edu.ar";
            this.FechaAlta = DateTime.Now;

        }

        public Persona() { }

    }

}
