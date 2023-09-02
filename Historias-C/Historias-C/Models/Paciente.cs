namespace Historias_C.Models
{
    public class Paciente : Persona
    {
        public string ObraSocial { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }

        public Paciente(string nombre, string apellido, int dni, int telefono, string direccion, string obraSocial, HistoriaClinica historiaClinica) : base(
             nombre, apellido, dni, telefono, direccion)
        {
            ObraSocial = obraSocial;
            HistoriaClinica = historiaClinica;
        }


    }
}
