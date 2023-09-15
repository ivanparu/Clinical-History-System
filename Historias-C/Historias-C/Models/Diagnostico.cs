using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Diagnostico
    { 
        private const string _reqMsgDiagnostico = "Es necesaria la {0} ,porfavor ingrese datos.";
        private const string _reqStrMinMaxDiagnostico = "Debe ser entre {2} y {1}";


        [Range(10, 20, ErrorMessage = _reqStrMinMaxDiagnostico)]
        public int Id { get; set; }
        public Epicrisis Epicrisis { get; set; }
        [Required(ErrorMessage = _reqMsgDiagnostico)]
        [StringLength(400,MinimumLength = 10,ErrorMessage = _reqStrMinMaxDiagnostico)]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = _reqMsgDiagnostico)]
        [StringLength(200, MinimumLength = 5, ErrorMessage = _reqStrMinMaxDiagnostico)]
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
