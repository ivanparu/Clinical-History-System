using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_C.Models
{
    public class Epicrisis
    {
     
        public int Id { get; set; }
        public Episodio Episodio { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]

        [ForeignKey("Episodio")]
        public int EpisodioId { get; set; }
        public Medico Medico { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        public int MedicoId { get; set; }

        public DateTime FechaYHora { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(400, MinimumLength = 10, ErrorMessage = ErrorMessages._reqStrMinMax)]
        [Display(Name ="Diagnostico")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(200, MinimumLength = 5, ErrorMessage = ErrorMessages._reqStrMinMax)]
        public string Recomendacion { get; set; }




    }
}
