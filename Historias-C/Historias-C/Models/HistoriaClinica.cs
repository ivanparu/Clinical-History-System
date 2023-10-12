using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_C.Models
{
    public class HistoriaClinica
    {
        private const string _reqMsg = "El campo {0} es requerido.";
        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";


        public int Id { get; set; }

       
        public Paciente Paciente { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public List<Episodio> Episodios { get; set; }
        

       

    }
}
