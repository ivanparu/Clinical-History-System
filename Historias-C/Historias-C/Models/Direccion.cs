namespace Historias_C.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Altura { get; set; }
        public string Barrio { get; set; }
        public string Ciudad { get; set; }

        public int PersonaId { get; set; }
        public Persona Persona { get; set; }





    }

}