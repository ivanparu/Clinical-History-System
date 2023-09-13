namespace Historias_C.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public String Descripcion { get; set; }
        public String Recomendacion { get; set; }


        public Diagnostico (int id,Epicrisis epicrisis, String descripcion, String recomendacion)
        {
            this.Id = id;   
            this.Epicrisis = epicrisis;   
            this.Descripcion = descripcion;
            this.Recomendacion = recomendacion;

        }


        public Diagnostico () { }







    }
}
