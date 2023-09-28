using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Diagnostico
    { 


        [Range(10, 20, ErrorMessage = ErrorMessages._reqStrMinMax)]
        public int Id { get; set; }
        public Epicrisis Epicrisis { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(400,MinimumLength = 10,ErrorMessage = ErrorMessages._reqStrMinMax)]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = ErrorMessages._reqMsg)]
        [StringLength(200, MinimumLength = 5,ErrorMessage = ErrorMessages._reqStrMinMax)]
        public String Recomendacion { get; set; }

        
        public Diagnostico()
        {

        }

        public Diagnostico (int id,Epicrisis epicrisis, String descripcion, String recomendacion)
        {
            this.Id = id;   
            this.Epicrisis = epicrisis;   
            this.Descripcion = descripcion;
            this.Recomendacion = recomendacion;

        }


  






    }
}
