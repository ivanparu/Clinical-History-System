using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_C.Models
{
    public class Paciente : Persona
    {
        [Required]
        public ObraSocial ObraSocial { get; set; }
        
       
        public HistoriaClinica HistoriaClinica { get; set; }


       

   



    }
}
