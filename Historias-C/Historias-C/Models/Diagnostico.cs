namespace Historias_C.Models
{
    public class Diagnostico
    {
        public int id { get; set; }
        public Epicrisis epicrisis { get; set; }
        public String descripcion { get; set; }
        public String recomendacion { get; set; }


        public Diagnostico(int id, Epicrisis epicrisis, String descripcion, String recomendacion)
        {
            this.id = id;
            this.epicrisis = epicrisis;
            this.descripcion = descripcion;
            this.recomendacion = recomendacion;

        }










    }
}
