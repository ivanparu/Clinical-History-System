namespace Historias_C.Models
{
    public class HistoriaClinica
    {

        public Paciente Paciente { get; set; }
        public List<Episodio> Episodio { get; set; }

        public HistoriaClinica(Paciente paciente, Episodio episodio)
        {
            Paciente = paciente;
           // Episodio = episodio;  revisar
        }

        public HistoriaClinica() { }
    }
}
