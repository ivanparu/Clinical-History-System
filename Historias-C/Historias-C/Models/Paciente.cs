namespace Historias_C.Models
{
    public class Paciente : Persona
    {
        public ObraSocial ObraSocial { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }

        public Paciente(string nombre, string apellido, int dni, int telefono, string direccion, int id, ObraSocial obraSocial, HistoriaClinica historiaClinica) : base(
             nombre, apellido, dni, telefono, direccion, id)
        {
            ObraSocial = ObraSocial;
            HistoriaClinica = historiaClinica;
        }


    }
}
