namespace Historias_C.Models
{
    public class Epicrisis
    {
        public int id { get; set; }
        public Episodio episodio; { get; set; }
        public Medico medico { get; set; }
        public DateTime fechaYHora { get; set; } = DateTime.Now;
        public Diagnostico diagnostico { get; set; }


        public Epicrisis(int id,Episodio episodio, Medico  medico, DateTime fechaYHora, Diagnostico diagnostico)
        {
            this.id = id;   
            this.episodio = episodio;
            this.medico = medico;   
            this.diagnostico = diagnostico;
            this.fechaYHora = fechaYHora;

        }



    }
}
