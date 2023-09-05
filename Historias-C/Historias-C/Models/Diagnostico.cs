namespace Historias_C.Models
{
    public class Diagnostico
    {
      int id { get; set; }
      Epicrisis epicrisis { get; }
      String descripcion { get; }
      String recomendacion { get; }   


        public Diagnostico (int id,Epicrisis epicrisis, String descripcion, String recomendacion)
        {
            this.id = id;   
            this.epicrisis = epicrisis;   
            this.descripcion = descripcion;
            this.recomendacion = recomendacion;

        }










    }
}
