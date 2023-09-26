namespace Historias_C.Models
{
    public class HistoriaClinica
    {

        public int Id { get; set; }

        public Paciente Paciente { get; set; }
        public int PacienteId { get; set; }
        public List<Episodio> Episodios { get; set; }

        public HistoriaClinica(Paciente paciente, Episodio episodio)
        {
            Paciente = paciente;
           // Episodio = episodio;  revisar
        }

        public HistoriaClinica() { }
    }
}
