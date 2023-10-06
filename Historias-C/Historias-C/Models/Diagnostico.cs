using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Diagnostico
    {







        public int Id { get; set; }
        public Epicrisis Epicrisis { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [Range(1, 30, ErrorMessage = ErrorMessages._reqRange)]
        [RegularExpression("^[0-9]+$", ErrorMessage = ErrorMessages._reqMsg)]
        public int IdEpicrisis { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(400,MinimumLength = 10,ErrorMessage = ErrorMessages._reqStrMinMax)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(200, MinimumLength = 5,ErrorMessage = ErrorMessages._reqStrMinMax)]
        public string Recomendacion { get; set; }

        
        public Diagnostico()
        {

        }

        public Diagnostico (int id,Epicrisis epicrisis, string descripcion, string recomendacion)
        {
            this.Id = id;   
            this.Epicrisis = epicrisis;   
            this.Descripcion = descripcion;
            this.Recomendacion = recomendacion;

        }


  






    }
}
