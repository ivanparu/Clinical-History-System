namespace Historias_C.Models
{
    public class Epicrisis
    {
        public int Id { get; set; }
        public Episodio Episodio; { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaYHora { get; set; } = DateTime.Now;
        public Diagnostico Diagnostico { get; set; }


        public Epicrisis(int id,Episodio episodio, Medico  medico, DateTime fechaYHora, Diagnostico diagnostico)
        {
            this.Id = id;   
            this.Episodio = episodio;
            this.Medico = medico;   
            this.Diagnostico = diagnostico;
            this.FechaYHora = fechaYHora;

        }

        public Epicrisis() { }

    }
}
