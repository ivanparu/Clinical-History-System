namespace Historias_C.Models
{
    public class Epicrisis
    {
        int id { get; set; }
        Episodio episodio;
        Medico medico { get; }
        DateTime fechaYHora { get; }
        Diagnostico diagnostico { get; }
                        

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
